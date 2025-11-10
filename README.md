public BicValidationResponse getBicFromIban(String iban) {
    BicValidationResponse response = new BicValidationResponse();
    response.setIban(iban);

    if (iban == null || iban.isEmpty()) {
        response.setStatus("INVALID_IBAN");
        return response;
    }

    Iban ibanValue;
    try {
        ibanValue = Iban.valueOf(iban);
    } catch (IbanFormatException | InvalidCheckDigitException | UnsupportedCountryException e) {
        LOGGER.debug("Invalid IBAN provided: {}", iban, e);
        response.setStatus("INVALID_IBAN");
        return response;
    }

    List<BankAgency> agencies = bankAgencyRepo.findByBankCodeAndCountryIso2(
            ibanValue.getBankCode(),
            ibanValue.getCountryCode().name()
    );

    if (agencies.isEmpty()) {
        LOGGER.debug("Unknown bankCode=<{}> branchCode=<{}> country=<{}>",
                ibanValue.getBankCode(),
                ibanValue.getBranchCode(),
                ibanValue.getCountryCode().name());
        response.setStatus("INVALID_IBAN");
        return response;
    }

    BankAgency bankAgency = agencies.get(0);
    if (bankAgency.getBankInfo() == null || bankAgency.getBankInfo().getBic() == null) {
        LOGGER.debug("No BIC found for bankCode=<{}>, country=<{}>",
                ibanValue.getBankCode(),
                ibanValue.getCountryCode().name());
        response.setStatus("INVALID_IBAN");
        return response;
    }

    response.setBic(bankAgency.getBankInfo().getBic());
    response.setStatus("VALID");
    return response;
}package eu.olkypay.bankInfo.service;

import com.fasterxml.jackson.databind.JsonNode;
import com.fasterxml.jackson.databind.ObjectMapper;
import com.neovisionaries.i18n.CountryCode;
import eu.olkypay.bankInfo.dto.BankInfoDTO;
import eu.olkypay.bankInfo.dto.BicValidationResponse;
import eu.olkypay.bankInfo.dto.IbanInfoUtilsDTO;
import eu.olkypay.bankInfo.dto.IbanSearchHistoryDTO;
import eu.olkypay.bankInfo.dto.externalapi.BankResponse;
import eu.olkypay.bankInfo.dto.externalapi.FindBankResponse;
import eu.olkypay.bankInfo.dto.externalapi.IbanValidationResponse;
import eu.olkypay.bankInfo.mapper.BankAgencyMapper;
import eu.olkypay.bankInfo.mapper.BankInfoMapper;
import eu.olkypay.bankInfo.mapper.IbanSearchHistoryMapper;
import eu.olkypay.bankInfo.model.BankAgency;
import eu.olkypay.bankInfo.model.BankInfo;
import eu.olkypay.bankInfo.model.IbanSearchHistory;
import eu.olkypay.bankInfo.repository.BankAgencyRepository;
import eu.olkypay.bankInfo.repository.BankInfoRepository;
import eu.olkypay.bankInfo.repository.IbanSearchHistoryRepository;
import lombok.extern.slf4j.Slf4j;
import org.iban4j.*;
import org.springframework.stereotype.Service;
import org.springframework.web.reactive.function.client.WebClient;
import org.springframework.web.reactive.function.client.WebClientResponseException;
import reactor.core.publisher.Mono;

import java.net.URI;
import java.time.Duration;
import java.time.LocalDateTime;
import java.util.List;
import java.util.Locale;
import java.util.Optional;

import static org.hibernate.sql.ast.SqlTreeCreationLogger.LOGGER;

@Slf4j
@Service
public class BankInfoValidationService {

    private final WebClient webClient;
    private final IbanSearchHistoryMapper searchHistorymapper;
    private final IbanSearchHistoryRepository searchHistoryRepo;
    private final BankAgencyRepository bankAgencyRepo;
    private final BankInfoRepository bankInfoRepo;
    private final ObjectMapper objectMapper;
    private final BankAgencyMapper bankAgencyMapper;
    private final BankInfoMapper bankInfoMapper;

    public BankInfoValidationService(WebClient webClient, IbanSearchHistoryMapper searchHistorymapper, IbanSearchHistoryRepository searchHistoryRepo, BankAgencyRepository bankAgencyRepo, BankInfoRepository bankInfo, ObjectMapper objectMapper, BankAgencyMapper bankAgencyMapper, BankInfoMapper bankInfoMapper) {
        this.webClient = webClient;
        this.searchHistorymapper = searchHistorymapper;
        this.searchHistoryRepo = searchHistoryRepo;
        this.bankAgencyRepo = bankAgencyRepo;
        this.bankInfoRepo = bankInfo;
        this.objectMapper = objectMapper;
        this.bankAgencyMapper = bankAgencyMapper;
        this.bankInfoMapper = bankInfoMapper;
    }

    public BicValidationResponse validateBic(String bic) {
        String cleanedBic = bic.replaceAll("\\s+", "");

        try {
            BicUtil.validate(cleanedBic); // Seule et unique validation ici
            return new BicValidationResponse("VALID", cleanedBic);
        } catch (Exception e) {
            return new BicValidationResponse("INVALID_BIC", cleanedBic);
        }
    }


    public BicValidationResponse validateIbanAndBic(String bic, String iban, IbanInfoUtilsDTO ibanInfo) {
        BicValidationResponse response = new BicValidationResponse();
        response.setBic(bic);
        response.setIban(iban);
        Iban ibanValue = Iban.valueOf(iban);
        List<BankAgency> agencies = bankAgencyRepo.findByBankCodeAndCountryIso2(
                ibanValue.getBankCode(),
                ibanValue.getCountryCode().name()
        );

        BankAgency bankAgency = agencies.isEmpty() ? null : agencies.get(0);
        if (bankAgency == null ) {
            log.debug("Unknown Bank Agency Info bankCode=<{}> branchCode=<{}> country=<{}>", ibanInfo.getBankCode(), ibanInfo.getBranchCode(),
                    ibanInfo.getCountry());
            response.setStatus("INVALID_IBAN");
            return response;
        }
        String bicOfBa= bankAgency.getBankInfo().getBic();
        String normalizedBicOfBa = bicOfBa.length() > 8 && bicOfBa.endsWith("XXX")
                ? bicOfBa.substring(0, 8)
                : bicOfBa;
        String normalizedBic = bic.length() > 8 && bic.endsWith("XXX")
                ? bic.substring(0, 8)
                : bic;

        if (!normalizedBicOfBa.equals(normalizedBic)) {
            log.info("Received Bic=<{}> Expected=<{}>", bic, bankAgency.getBankInfo().getBic());
            response.setStatus("BIC_MISMATCH");
            return response;
        }
        response.setStatus("VALID");
        return response;
    }

    public BicValidationResponse getBicFromIban(String iban) {
        Iban ibanValue = null;
        BicValidationResponse response = new BicValidationResponse();
        response.setIban(iban);
        try {
            ibanValue = Iban.valueOf(iban);
        } catch (IbanFormatException | InvalidCheckDigitException | UnsupportedCountryException e) {
            if (LOGGER.isDebugEnabled()) {
                LOGGER.debug("Invalid iban provided", e);
            }
            response.setStatus("INVALID_IBAN");

            return response;
        }

        response.setIban(iban);
        if (iban.isEmpty()) {
            return response;
        }
        List<BankAgency> agencies = bankAgencyRepo.findByBankCodeAndCountryIso2(
                ibanValue.getBankCode(),
                ibanValue.getCountryCode().name()
        );
        BankAgency bankAgency = agencies.isEmpty() ? null : agencies.get(0);
        if (bankAgency == null || bankAgency.getBankInfo() == null) {
            log.debug("Unknown bankCode=<{}> branchCode=<{}> country=<{}>", ibanValue.getBranchCode(), ibanValue.getBranchCode(),
                    ibanValue.getCountryCode().name());
            response.setStatus("INVALID_IBAN");
            return response;
        }
        response.setBic(bankAgency.getBankInfo().getBic());
        response.setStatus("VALID");
        return response;
    }

    public Optional<IbanSearchHistoryDTO> validateIban(String iban) {
        String ibanFormatted = iban.replaceAll("\\s+", "");
        try {
            IbanUtil.validate(ibanFormatted);
        } catch (IbanFormatException ex) {
            throw new IbanFormatException("Invalid IBAN format: " + iban, ex);
        }
        Iban ibanValue = Iban.valueOf(ibanFormatted);
        List<BankAgency> agencies = bankAgencyRepo.findByBankCodeAndCountryIso2(
                ibanValue.getBankCode(),
                ibanValue.getCountryCode().name()
        );

        BankAgency bankAgency = agencies.isEmpty() ? null : agencies.get(0);

        if (bankAgency != null) {
            log.info("IBAN found in database and is up-to-date.");
            IbanSearchHistoryDTO dto = new IbanSearchHistoryDTO();
            dto.setIban(iban);
            dto.setBankInfoDTO(bankInfoMapper.toDto(bankAgency.getBankInfo()));
            dto.setBankAgencyDTO(bankAgencyMapper.toDTO(bankAgency));
            dto.setResult("VALID");
            return Optional.of(dto);
        }
        Optional<IbanSearchHistory> ibanSearchHistory = searchHistoryRepo.findByIban(ibanFormatted);
        if (ibanSearchHistory.isPresent()) {
            IbanSearchHistory existingRecord = ibanSearchHistory.get();
            boolean isUpToDate = existingRecord.getUpdatedAt()
                    .isAfter(LocalDateTime.now().minusDays(300));

            if (isUpToDate) {
                log.info("IBAN found in database and is up-to-date.");
                return Optional.of(searchHistorymapper.toDTO(existingRecord));
            }

            log.info("IBAN found in database but outdated. Calling external API for an update.");
            return updateFromExternalApi(ibanFormatted, existingRecord);
        }

        log.info("IBAN not found in database. Calling external API.");
        return fetchFromExternalApi(ibanFormatted);
    }

    private Optional<IbanSearchHistoryDTO> updateFromExternalApi(String ibanFormatted, IbanSearchHistory existingRecord) {
        IbanValidationResponse response = callIbanValidationApi(ibanFormatted).block();
        if (response == null) {
            log.error("Failed to call external API.");
            return Optional.empty();
        }
        // Update or create BankInfo using its mapper
        BankInfo bankInfo = bankInfoRepo.findByBic(response.getBicCandidates().getFirst().getBic()).orElse(null);
        if (bankInfo != null) {
            bankInfoMapper.updateEntityFromResponse(response, bankInfo);
            bankInfoRepo.save(bankInfo);
        } else {
            bankInfo = bankInfoMapper.toEntity(response);
            bankInfoRepo.save(bankInfo);
        }
        // Update or create BankAgency using its mapper
        BankAgency bankAgency = bankAgencyRepo
                .findByBranchCodeAndBankCodeAndCountryIso2(response.getBranchCode(), response.getBankCode(), response.getCountry())
                .orElse(null);
        if (bankAgency != null) {
            bankAgencyMapper.updateEntityFromResponse(response, bankAgency);
            bankAgencyRepo.save(bankAgency);
        } else {
            bankAgency = bankAgencyMapper.toEntity(response);
            bankAgencyRepo.save(bankAgency);
        }
        existingRecord.setResult(response.getResult());
        existingRecord.setBankAgency(bankAgency);
        existingRecord.setUpdatedAt(LocalDateTime.now());
        searchHistoryRepo.save(existingRecord);
        return Optional.of(searchHistorymapper.toDTO(existingRecord));
    }


    private Optional<IbanSearchHistoryDTO> fetchFromExternalApi(String ibanFormatted) {
        IbanValidationResponse response = callIbanValidationApi(ibanFormatted).block();
        if (response == null) {
            log.error("Failed to retrieve data from the external API.");
            return Optional.empty();
        }
        // Create or update BankInfo using its mapper
        BankInfo bankInfo = bankInfoRepo.findByBic(response.getBicCandidates().getFirst().getBic()).orElse(null);
        if (bankInfo == null) {
            bankInfo = bankInfoMapper.toEntity(response);
            bankInfo.setLocation(response.getCountry() != null ? Locale.of("",response.getCountry()).getDisplayCountry(): "");
            bankInfoRepo.save(bankInfo);
        } else if(bankInfo.getUpdatedAt().isAfter(LocalDateTime.now().minusDays(300))){
            bankInfoMapper.updateEntityFromResponse(response, bankInfo);
            bankInfoRepo.save(bankInfo);
        }
        BankAgency bankAgency = bankAgencyRepo
                .findByBranchCodeAndBankCodeAndCountryIso2(response.getBranchCode(), response.getBankCode(), response.getCountry())
                .orElse(null);
        if (bankAgency == null) {
            bankAgency = bankAgencyMapper.toEntity(response);
            bankAgency.setBankInfo(bankInfo);
            bankAgencyRepo.save(bankAgency);
        } else {
            bankAgencyMapper.updateEntityFromResponse(response, bankAgency);
            bankAgencyRepo.save(bankAgency);
        }
        IbanSearchHistory entity = new IbanSearchHistory(ibanFormatted, response.getResult(), response.toString(),
                LocalDateTime.now(), bankAgency);
        searchHistoryRepo.save(entity);
        return Optional.of(searchHistorymapper.toDTO(entity));
    }


    public Optional<BankInfoDTO> findBankByBic(String bic) {
        String bicFormatted = bic.replaceAll("\\s+", "");
        try {
            BicUtil.validate(bic);
        } catch (Exception e) {
            return Optional.of(new BankInfoDTO("INVALID_BIC", bic));
        }
        Optional<BankInfo> existingRecord = bankInfoRepo.findByBic(bicFormatted);
        if (existingRecord.isPresent()) {
            log.info("BIC found in database.");
            Optional<BankInfoDTO> bankInfoDTO = existingRecord.map(bankInfoMapper::toDto);
            bankInfoDTO.get().setStatus("VALID");
            bankInfoDTO.get().setName(bankInfoDTO.get().getInstitution());
            String isoCode = bankInfoDTO.get().getCountryCode();
            if (isoCode != null && !isoCode.isBlank()) {
                CountryCode code = CountryCode.getByCode(isoCode.trim().toUpperCase());
                if (code != null) {
                    bankInfoDTO.get().setCountryName(code.getName());
                }
            }
            return bankInfoDTO;
        }

        log.info("BIC not found in database. Calling external API.");
        FindBankResponse bankResponse = callFindBankApi(bicFormatted).block();
        if (bankResponse == null || bankResponse.getBanks().isEmpty()) {
            log.warn("API response was null or empty, returning empty.");
            return Optional.empty();
        }
        BankResponse response = bankResponse.getBanks().getFirst();
        BankInfo bankInfo = bankInfoRepo.findByBic(response.getBic()).orElse(null);
        if (bankInfo == null) {
            bankInfo = bankInfoMapper.toEntity(response);
            bankInfo.setLocation(Locale.of("",response.getCountry()).getDisplayCountry());
            bankInfo.setSearchResult(response.toString());
            bankInfoRepo.save(bankInfo);
            // Element to change if bankInfo exist on db
        } else if (bankInfo.getUpdatedAt().isAfter(LocalDateTime.now().minusDays(300))) {
            bankInfo.setAddress1(!response.getAddress().isEmpty() ? response.getAddress() : bankInfo.getAddress1());
            bankInfo.setUpdatedAt(LocalDateTime.now());
            bankInfoRepo.save(bankInfo);
        }
        BankAgency bankAgency = bankAgencyRepo.findByBranchCodeAndBankCodeAndCountryIso2(
                response.getBranchcode(), response.getBankcode(), response.getCountry()).orElse(null);
        if (bankAgency == null) {
            bankAgency = bankAgencyMapper.toEntity(response);
            bankAgency.setBankInfo(bankInfo);
            bankAgencyRepo.save(bankAgency);
        } else {
            bankAgency.setBankInfo(bankInfo);
            bankAgencyRepo.save(bankAgency);
        }
        Optional<BankInfoDTO> searchHistoryDTO = Optional.of(bankInfoMapper.toDto(bankInfo));
        searchHistoryDTO.get().setStatus("VALID");
        return searchHistoryDTO;
    }

    private Mono<IbanValidationResponse> callIbanValidationApi(String iban) {
        String valditeIbanURL = "/validate_iban/{iban}";
        log.info("Init Call Sepa URL");
        log.info("http.proxyHost = {}", System.getProperty("http.proxyHost"));
        log.info("http.proxyPort = {}", System.getProperty("http.proxyPort"));
        log.info("https.proxyHost = {}", System.getProperty("https.proxyHost"));
        log.info("https.proxyPort = {}", System.getProperty("https.proxyPort"));
        log.info("http.nonProxyHosts = {}", System.getProperty("http.nonProxyHosts"));
        return webClient.get().uri(valditeIbanURL, iban.trim())
                .retrieve()
                .bodyToMono(IbanValidationResponse.class)
                .timeout(Duration.ofSeconds(10))
                .onErrorResume(WebClientResponseException.class, ex -> {
                    log.error("Exception during WebClient call: Status = {}, Body = {}, Headers = {}",
                            ex.getStatusCode(), ex.getResponseBodyAsString(), ex.getHeaders());
                    return Mono.error(new RuntimeException("Error while validating IBAN : " + ex.getMessage(), ex));
                });
    }

    public Mono<FindBankResponse> callFindBankApi(String bic) {
        String findBankUrl = "/find_bank/ / /{bic}";
        return webClient.get().uri(uriBuilder -> {
                    URI uri = uriBuilder.path(findBankUrl).build(bic);
                    log.info("Calling external API with full URL: {}", uri);
                    return uri;
                })
                .retrieve()
                .bodyToMono(JsonNode.class)
//          .doOnNext(jsonNode -> log.info("Parsed JSON: {}", jsonNode))
                .flatMap(this::mapJsonToFindBankResponse)
                .onErrorResume(WebClientResponseException.class, ex ->
                        Mono.error(new RuntimeException("Error while validating BIC : " + ex.getMessage())));
    }

    private Mono<FindBankResponse> mapJsonToFindBankResponse(JsonNode jsonNode) {
        try {
            String result = jsonNode.path("result").asText("");
            int balance = jsonNode.path("balance").asInt(0);
            JsonNode banksNode = jsonNode.path("banks");
            if (!banksNode.isArray() || banksNode.isEmpty()) {
                return Mono.just(new FindBankResponse(result, List.of(), balance));
            }
            JsonNode firstBankNode = banksNode.get(0);
            BankResponse bankResponse = objectMapper.treeToValue(firstBankNode, BankResponse.class);
            return Mono.just(new FindBankResponse(result, List.of(bankResponse), balance));
        } catch (Exception e) {
            log.error("Error mapping JSON to DTO: {}", e.getMessage(), e);
            return Mono.empty();
        }
    }
}
i need commen method for validating iban to be refoctor this can you the refoactoring and tell me whch class tyou refactor
