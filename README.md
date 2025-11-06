niveau droits et owner vous voulez mettre quoi pour les jar et les .env dans les dossiers de déploiement des apps? par exemple pour bank-info j'ai setup ça

-rw-r-----. 1 bank-info bank-info      568 Jul 29 09:41 bank-info.env
-rwxr-----. 1 bank-info bank-info 75728247 Nov  6 15:32 bank-info.jar
-rw-r-----. 1 bank-info bank-info      758 Jul 29 09:51 java.env
3:54 PM
je sais pas si les .env doivent avoir root en owner




2 replies

Following
Last reply now
les permissions j'ai mis 640 pour les .env et 750 pour les .jar (sur l'exemple c'est 740 mais c'est pas normal jpense c'est un ancien script)




je coimprens pas l'echage peux tu m'expliquer les trucs owner command 


    public static Iban valueOf(String iban) throws IbanFormatException, InvalidCheckDigitException, CountryException {
        if (LOGGER.isDebugEnabled()) {
            LOGGER.debug("valueOf({})", iban);
        }

        if (!Strings.isEmpty(iban) && iban.length() >= 4) {
            String normalizedIban = Strings.deleteWhitespace(iban);

            CountryCode countryCode;
            try {
                countryCode = CountryCode.valueOf(normalizedIban.substring(0, 2));
            } catch (IllegalArgumentException var8) {
                throw new CountryException(normalizedIban.substring(0, 2), "No Country for code [" + normalizedIban.substring(0, 2) + "]");
            }

            String checkDigit = normalizedIban.substring(2, 4);
            String expectedCheckDigit = IbanUtil.calculateCheckDigit(normalizedIban);
            if (!checkDigit.equals(expectedCheckDigit)) {
                throw new InvalidCheckDigitException(checkDigit, expectedCheckDigit, "Invalid check digit for iban=[" + iban + "] expected=" + expectedCheckDigit);
            } else {
                BbanStruct struct = BbanStruct.forCountry(countryCode);
                BbanStructInstance[] instances = struct.instance(normalizedIban.toCharArray(), 4);

                for(int i = 0; i < instances.length; ++i) {
                    if (instances[i] == null) {
                        String var10002 = String.valueOf(struct.entries()[i].type());
                        throw new IbanException("Missing [" + var10002 + "] for iban [" + normalizedIban + "]");
                    }
                }

                return new Iban(countryCode, checkDigit, instances);
            }
        } else {
            throw new IbanFormatException(IbanFormatViolation.IBAN_NOT_NULL, "iban null ! [" + iban + "]");
        }
    }

    //
// Source code recreated from a .class file by IntelliJ IDEA
// (powered by FernFlower decompiler)
//

package eu.olkypay.framework.iban;

import java.util.EnumMap;
import java.util.Map;
import java.util.function.Function;

public final class BbanStruct {
    private static final Map<CountryCode, BbanStruct> STRUCTURES = new EnumMap(CountryCode.class);
    private static final Map<CountryCode, Function<BbanStructInstance[], char[]>> NATIONAL_CHECK_DIGITS;
    private final CountryCode _country;
    private final int _bankCode;
    private final int _branchCode;
    private final int _accountNumber;
    private final int _nationalCheckDigit;
    private final int _accountType;
    private final int _ownerAccountNumber;
    private final int _identificationNumber;
    private final BbanStructEntry[] _entries;
    private final int _length;

    private BbanStruct(CountryCode country, BbanStructEntry... entries) {
        this._country = country;
        this._entries = entries;
        int length = 0;
        int[] indexes = new int[]{-1, -1, -1, -1, -1, -1, -1};

        for(int i = 0; i < entries.length; ++i) {
            switch (entries[i].type()) {
                case BANK_CODE -> indexes[0] = i;
                case BRANCH_CODE -> indexes[1] = i;
                case ACCOUNT_NUMBER -> indexes[2] = i;
                case NATIONAL_CHECK_DIGIT -> indexes[3] = i;
                case ACCOUNT_TYPE -> indexes[4] = i;
                case OWNER_ACCOUNT_NUMBER -> indexes[5] = i;
                case IDENTIFICATION_NUMBER -> indexes[6] = i;
            }

            length += entries[i].length();
        }

        this._bankCode = indexes[0];
        this._branchCode = indexes[1];
        this._accountNumber = indexes[2];
        this._nationalCheckDigit = indexes[3];
        this._accountType = indexes[4];
        this._ownerAccountNumber = indexes[5];
        this._identificationNumber = indexes[6];
        this._length = length;
    }

    public CountryCode countryCode() {
        return this._country;
    }

    public BbanStructEntry bankCode() {
        return this._entries[this._bankCode];
    }

    public int bankCodeIndex() {
        return this._bankCode;
    }

    public BbanStructEntry branchCode() {
        return this._entries[this._branchCode];
    }

    public int branchCodeIndex() {
        return this._branchCode;
    }

    public BbanStructEntry accountNumber() {
        return this._entries[this._accountNumber];
    }

    public int accountNumberIndex() {
        return this._accountNumber;
    }

    public BbanStructEntry nationalCheckDigit() {
        return this._entries[this._nationalCheckDigit];
    }

    public int nationalCheckDigitIndex() {
        return this._nationalCheckDigit;
    }

    public BbanStructEntry accountType() {
        return this._entries[this._accountType];
    }

    public int accountTypeIndex() {
        return this._accountType;
    }

    public BbanStructEntry ownerAccountNumber() {
        return this._entries[this._ownerAccountNumber];
    }

    public int ownerAccountNumberIndex() {
        return this._ownerAccountNumber;
    }

    public BbanStructEntry identificationNumber() {
        return this._entries[this._identificationNumber];
    }

    public int identificationNumberIndex() {
        return this._identificationNumber;
    }

    protected BbanStructEntry[] entries() {
        return this._entries;
    }

    public int length() {
        return this._length;
    }

    public static BbanStruct forCountry(CountryCode countryCode) {
        BbanStruct struct = (BbanStruct)STRUCTURES.get(countryCode);
        if (struct == null) {
            throw new CountryException(countryCode.name(), "No Bban struct for country [" + countryCode.name() + "]");
        } else {
            return struct;
        }
    }

    public static Function<BbanStructInstance[], char[]> checkDigitforCountry(CountryCode countryCode) {
        return (Function)NATIONAL_CHECK_DIGITS.get(countryCode);
    }

    public BbanStructInstance[] instance(char[] charArray, int off, boolean validateChars) {
        BbanStructInstance[] instances = new BbanStructInstance[this._entries.length];
        int offset = off;

        for(int i = 0; i < this._entries.length; ++i) {
            instances[i] = this._entries[i].fromValue(charArray, offset, validateChars);
            offset += this._entries[i].length();
        }

        return instances;
    }

    public BbanStructInstance[] instance(char[] charArray, int off) {
        return this.instance(charArray, off, true);
    }

    static {
        STRUCTURES.put(CountryCode.AL, new BbanStruct(CountryCode.AL, new BbanStructEntry[]{new BbanStructEntry(BbanEntryType.BANK_CODE, CharacterType.N, 0, 3), new BbanStructEntry(BbanEntryType.BRANCH_CODE, CharacterType.N, 3, 4), new BbanStructEntry(BbanEntryType.NATIONAL_CHECK_DIGIT, CharacterType.N, 7, 1), new BbanStructEntry(BbanEntryType.ACCOUNT_NUMBER, CharacterType.C, 8, 16)}));
        STRUCTURES.put(CountryCode.AD, new BbanStruct(CountryCode.AD, new BbanStructEntry[]{new BbanStructEntry(BbanEntryType.BANK_CODE, CharacterType.N, 0, 4), new BbanStructEntry(BbanEntryType.BRANCH_CODE, CharacterType.N, 4, 4), new BbanStructEntry(BbanEntryType.ACCOUNT_NUMBER, CharacterType.C, 8, 12)}));
        STRUCTURES.put(CountryCode.AT, new BbanStruct(CountryCode.AT, new BbanStructEntry[]{new BbanStructEntry(BbanEntryType.BANK_CODE, CharacterType.N, 0, 5), new BbanStructEntry(BbanEntryType.ACCOUNT_NUMBER, CharacterType.N, 5, 11)}));
        STRUCTURES.put(CountryCode.AZ, new BbanStruct(CountryCode.AZ, new BbanStructEntry[]{new BbanStructEntry(BbanEntryType.BANK_CODE, CharacterType.A, 0, 4), new BbanStructEntry(BbanEntryType.ACCOUNT_NUMBER, CharacterType.C, 4, 20)}));
        STRUCTURES.put(CountryCode.BH, new BbanStruct(CountryCode.BH, new BbanStructEntry[]{new BbanStructEntry(BbanEntryType.BANK_CODE, CharacterType.A, 0, 4), new BbanStructEntry(BbanEntryType.ACCOUNT_NUMBER, CharacterType.N, 4, 14)}));
        STRUCTURES.put(CountryCode.BA, new BbanStruct(CountryCode.BA, new BbanStructEntry[]{new BbanStructEntry(BbanEntryType.BANK_CODE, CharacterType.N, 0, 3), new BbanStructEntry(BbanEntryType.BRANCH_CODE, CharacterType.N, 3, 3), new BbanStructEntry(BbanEntryType.ACCOUNT_NUMBER, CharacterType.N, 6, 8), new BbanStructEntry(BbanEntryType.NATIONAL_CHECK_DIGIT, CharacterType.N, 14, 2)}));
        STRUCTURES.put(CountryCode.BE, new BbanStruct(CountryCode.BE, new BbanStructEntry[]{new BbanStructEntry(BbanEntryType.BANK_CODE, CharacterType.N, 0, 3), new BbanStructEntry(BbanEntryType.ACCOUNT_NUMBER, CharacterType.N, 3, 7), new BbanStructEntry(BbanEntryType.NATIONAL_CHECK_DIGIT, CharacterType.N, 10, 2)}));
        STRUCTURES.put(CountryCode.BR, new BbanStruct(CountryCode.BR, new BbanStructEntry[]{new BbanStructEntry(BbanEntryType.BANK_CODE, CharacterType.N, 0, 8), new BbanStructEntry(BbanEntryType.BRANCH_CODE, CharacterType.N, 8, 5), new BbanStructEntry(BbanEntryType.ACCOUNT_NUMBER, CharacterType.N, 13, 10), new BbanStructEntry(BbanEntryType.ACCOUNT_TYPE, CharacterType.A, 23, 1), new BbanStructEntry(BbanEntryType.OWNER_ACCOUNT_NUMBER, CharacterType.C, 24, 1)}));
        STRUCTURES.put(CountryCode.BG, new BbanStruct(CountryCode.BG, new BbanStructEntry[]{new BbanStructEntry(BbanEntryType.BANK_CODE, CharacterType.A, 0, 4), new BbanStructEntry(BbanEntryType.BRANCH_CODE, CharacterType.N, 4, 4), new BbanStructEntry(BbanEntryType.ACCOUNT_TYPE, CharacterType.N, 8, 2), new BbanStructEntry(BbanEntryType.ACCOUNT_NUMBER, CharacterType.C, 10, 8)}));
        STRUCTURES.put(CountryCode.HR, new BbanStruct(CountryCode.HR, new BbanStructEntry[]{new BbanStructEntry(BbanEntryType.BANK_CODE, CharacterType.N, 0, 7), new BbanStructEntry(BbanEntryType.ACCOUNT_NUMBER, CharacterType.C, 7, 10)}));
        STRUCTURES.put(CountryCode.CY, new BbanStruct(CountryCode.CY, new BbanStructEntry[]{new BbanStructEntry(BbanEntryType.BANK_CODE, CharacterType.N, 0, 3), new BbanStructEntry(BbanEntryType.BRANCH_CODE, CharacterType.N, 3, 5), new BbanStructEntry(BbanEntryType.ACCOUNT_NUMBER, CharacterType.C, 8, 16)}));
        STRUCTURES.put(CountryCode.CR, new BbanStruct(CountryCode.CR, new BbanStructEntry[]{new BbanStructEntry(BbanEntryType.BANK_CODE, CharacterType.N, 0, 3), new BbanStructEntry(BbanEntryType.ACCOUNT_NUMBER, CharacterType.N, 3, 14)}));
        STRUCTURES.put(CountryCode.DE, new BbanStruct(CountryCode.DE, new BbanStructEntry[]{new BbanStructEntry(BbanEntryType.BANK_CODE, CharacterType.N, 0, 8), new BbanStructEntry(BbanEntryType.ACCOUNT_NUMBER, CharacterType.N, 8, 10)}));
        STRUCTURES.put(CountryCode.CZ, new BbanStruct(CountryCode.CZ, new BbanStructEntry[]{new BbanStructEntry(BbanEntryType.BANK_CODE, CharacterType.N, 0, 4), new BbanStructEntry(BbanEntryType.ACCOUNT_NUMBER, CharacterType.N, 4, 16)}));
        STRUCTURES.put(CountryCode.DK, new BbanStruct(CountryCode.DK, new BbanStructEntry[]{new BbanStructEntry(BbanEntryType.BANK_CODE, CharacterType.N, 0, 4), new BbanStructEntry(BbanEntryType.ACCOUNT_NUMBER, CharacterType.N, 4, 10)}));
        STRUCTURES.put(CountryCode.DO, new BbanStruct(CountryCode.DO, new BbanStructEntry[]{new BbanStructEntry(BbanEntryType.BANK_CODE, CharacterType.C, 0, 4), new BbanStructEntry(BbanEntryType.ACCOUNT_NUMBER, CharacterType.N, 4, 20)}));
        STRUCTURES.put(CountryCode.EE, new BbanStruct(CountryCode.EE, new BbanStructEntry[]{new BbanStructEntry(BbanEntryType.BANK_CODE, CharacterType.N, 0, 2), new BbanStructEntry(BbanEntryType.BRANCH_CODE, CharacterType.N, 2, 2), new BbanStructEntry(BbanEntryType.ACCOUNT_NUMBER, CharacterType.N, 4, 11), new BbanStructEntry(BbanEntryType.NATIONAL_CHECK_DIGIT, CharacterType.N, 15, 1)}));
        STRUCTURES.put(CountryCode.FI, new BbanStruct(CountryCode.FI, new BbanStructEntry[]{new BbanStructEntry(BbanEntryType.BANK_CODE, CharacterType.N, 0, 3), new BbanStructEntry(BbanEntryType.BRANCH_CODE, CharacterType.N, 3, 3), new BbanStructEntry(BbanEntryType.ACCOUNT_NUMBER, CharacterType.N, 6, 7), new BbanStructEntry(BbanEntryType.NATIONAL_CHECK_DIGIT, CharacterType.N, 13, 1)}));
        STRUCTURES.put(CountryCode.ES, new BbanStruct(CountryCode.ES, new BbanStructEntry[]{new BbanStructEntry(BbanEntryType.BANK_CODE, CharacterType.N, 0, 4), new BbanStructEntry(BbanEntryType.BRANCH_CODE, CharacterType.N, 4, 4), new BbanStructEntry(BbanEntryType.NATIONAL_CHECK_DIGIT, CharacterType.N, 8, 2), new BbanStructEntry(BbanEntryType.ACCOUNT_NUMBER, CharacterType.N, 10, 10)}));
        STRUCTURES.put(CountryCode.GE, new BbanStruct(CountryCode.GE, new BbanStructEntry[]{new BbanStructEntry(BbanEntryType.BANK_CODE, CharacterType.A, 0, 2), new BbanStructEntry(BbanEntryType.ACCOUNT_NUMBER, CharacterType.N, 2, 16)}));
        STRUCTURES.put(CountryCode.GI, new BbanStruct(CountryCode.GI, new BbanStructEntry[]{new BbanStructEntry(BbanEntryType.BANK_CODE, CharacterType.A, 0, 4), new BbanStructEntry(BbanEntryType.ACCOUNT_NUMBER, CharacterType.C, 4, 15)}));
        STRUCTURES.put(CountryCode.GL, new BbanStruct(CountryCode.GL, new BbanStructEntry[]{new BbanStructEntry(BbanEntryType.BANK_CODE, CharacterType.N, 0, 4), new BbanStructEntry(BbanEntryType.ACCOUNT_NUMBER, CharacterType.N, 4, 9), new BbanStructEntry(BbanEntryType.NATIONAL_CHECK_DIGIT, CharacterType.N, 13, 1)}));
        STRUCTURES.put(CountryCode.GR, new BbanStruct(CountryCode.GR, new BbanStructEntry[]{new BbanStructEntry(BbanEntryType.BANK_CODE, CharacterType.N, 0, 3), new BbanStructEntry(BbanEntryType.BRANCH_CODE, CharacterType.N, 3, 4), new BbanStructEntry(BbanEntryType.ACCOUNT_NUMBER, CharacterType.C, 7, 16)}));
        STRUCTURES.put(CountryCode.GT, new BbanStruct(CountryCode.GT, new BbanStructEntry[]{new BbanStructEntry(BbanEntryType.BANK_CODE, CharacterType.C, 0, 4), new BbanStructEntry(BbanEntryType.ACCOUNT_NUMBER, CharacterType.C, 4, 20)}));
        STRUCTURES.put(CountryCode.HU, new BbanStruct(CountryCode.HU, new BbanStructEntry[]{new BbanStructEntry(BbanEntryType.BANK_CODE, CharacterType.N, 0, 3), new BbanStructEntry(BbanEntryType.BRANCH_CODE, CharacterType.N, 3, 4), new BbanStructEntry(BbanEntryType.ACCOUNT_NUMBER, CharacterType.N, 7, 16), new BbanStructEntry(BbanEntryType.NATIONAL_CHECK_DIGIT, CharacterType.N, 23, 1)}));
        STRUCTURES.put(CountryCode.FR, new BbanStruct(CountryCode.FR, new BbanStructEntry[]{new BbanStructEntry(BbanEntryType.BANK_CODE, CharacterType.N, 0, 5), new BbanStructEntry(BbanEntryType.BRANCH_CODE, CharacterType.N, 5, 5), new BbanStructEntry(BbanEntryType.ACCOUNT_NUMBER, CharacterType.C, 10, 11), new BbanStructEntry(BbanEntryType.NATIONAL_CHECK_DIGIT, CharacterType.N, 21, 2)}));
        STRUCTURES.put(CountryCode.FO, new BbanStruct(CountryCode.FO, new BbanStructEntry[]{new BbanStructEntry(BbanEntryType.BANK_CODE, CharacterType.N, 0, 4), new BbanStructEntry(BbanEntryType.ACCOUNT_NUMBER, CharacterType.N, 4, 9), new BbanStructEntry(BbanEntryType.NATIONAL_CHECK_DIGIT, CharacterType.N, 13, 1)}));
        STRUCTURES.put(CountryCode.IS, new BbanStruct(CountryCode.IS, new BbanStructEntry[]{new BbanStructEntry(BbanEntryType.BANK_CODE, CharacterType.N, 0, 4), new BbanStructEntry(BbanEntryType.NATIONAL_CHECK_DIGIT, CharacterType.N, 4, 2), new BbanStructEntry(BbanEntryType.ACCOUNT_NUMBER, CharacterType.N, 6, 16)}));
        STRUCTURES.put(CountryCode.IE, new BbanStruct(CountryCode.IE, new BbanStructEntry[]{new BbanStructEntry(BbanEntryType.BANK_CODE, CharacterType.A, 0, 4), new BbanStructEntry(BbanEntryType.BRANCH_CODE, CharacterType.N, 4, 6), new BbanStructEntry(BbanEntryType.ACCOUNT_NUMBER, CharacterType.N, 10, 8)}));
        STRUCTURES.put(CountryCode.IL, new BbanStruct(CountryCode.IL, new BbanStructEntry[]{new BbanStructEntry(BbanEntryType.BANK_CODE, CharacterType.N, 0, 3), new BbanStructEntry(BbanEntryType.BRANCH_CODE, CharacterType.N, 3, 3), new BbanStructEntry(BbanEntryType.ACCOUNT_NUMBER, CharacterType.N, 6, 13)}));
        STRUCTURES.put(CountryCode.IT, new BbanStruct(CountryCode.IT, new BbanStructEntry[]{new BbanStructEntry(BbanEntryType.NATIONAL_CHECK_DIGIT, CharacterType.A, 0, 1), new BbanStructEntry(BbanEntryType.BANK_CODE, CharacterType.N, 1, 5), new BbanStructEntry(BbanEntryType.BRANCH_CODE, CharacterType.N, 6, 5), new BbanStructEntry(BbanEntryType.ACCOUNT_NUMBER, CharacterType.C, 11, 12)}));
        STRUCTURES.put(CountryCode.JO, new BbanStruct(CountryCode.JO, new BbanStructEntry[]{new BbanStructEntry(BbanEntryType.BANK_CODE, CharacterType.A, 0, 4), new BbanStructEntry(BbanEntryType.BRANCH_CODE, CharacterType.N, 4, 4), new BbanStructEntry(BbanEntryType.ACCOUNT_NUMBER, CharacterType.C, 8, 18)}));
        STRUCTURES.put(CountryCode.KZ, new BbanStruct(CountryCode.KZ, new BbanStructEntry[]{new BbanStructEntry(BbanEntryType.BANK_CODE, CharacterType.N, 0, 3), new BbanStructEntry(BbanEntryType.ACCOUNT_NUMBER, CharacterType.C, 3, 13)}));
        STRUCTURES.put(CountryCode.KW, new BbanStruct(CountryCode.KW, new BbanStructEntry[]{new BbanStructEntry(BbanEntryType.BANK_CODE, CharacterType.A, 0, 4), new BbanStructEntry(BbanEntryType.ACCOUNT_NUMBER, CharacterType.C, 4, 22)}));
        STRUCTURES.put(CountryCode.LV, new BbanStruct(CountryCode.LV, new BbanStructEntry[]{new BbanStructEntry(BbanEntryType.BANK_CODE, CharacterType.A, 0, 4), new BbanStructEntry(BbanEntryType.ACCOUNT_NUMBER, CharacterType.C, 4, 13)}));
        STRUCTURES.put(CountryCode.LB, new BbanStruct(CountryCode.LB, new BbanStructEntry[]{new BbanStructEntry(BbanEntryType.BANK_CODE, CharacterType.N, 0, 4), new BbanStructEntry(BbanEntryType.ACCOUNT_NUMBER, CharacterType.C, 4, 20)}));
        STRUCTURES.put(CountryCode.LI, new BbanStruct(CountryCode.LI, new BbanStructEntry[]{new BbanStructEntry(BbanEntryType.BANK_CODE, CharacterType.N, 0, 5), new BbanStructEntry(BbanEntryType.ACCOUNT_NUMBER, CharacterType.C, 5, 12)}));
        STRUCTURES.put(CountryCode.LT, new BbanStruct(CountryCode.LT, new BbanStructEntry[]{new BbanStructEntry(BbanEntryType.BANK_CODE, CharacterType.N, 0, 5), new BbanStructEntry(BbanEntryType.ACCOUNT_NUMBER, CharacterType.C, 5, 11)}));
        STRUCTURES.put(CountryCode.LU, new BbanStruct(CountryCode.LU, new BbanStructEntry[]{new BbanStructEntry(BbanEntryType.BANK_CODE, CharacterType.N, 0, 3), new BbanStructEntry(BbanEntryType.ACCOUNT_NUMBER, CharacterType.C, 3, 11)}));
        STRUCTURES.put(CountryCode.LU, new BbanStruct(CountryCode.LU, new BbanStructEntry[]{new BbanStructEntry(BbanEntryType.BANK_CODE, CharacterType.N, 0, 3), new BbanStructEntry(BbanEntryType.ACCOUNT_NUMBER, CharacterType.C, 3, 13)}));
        STRUCTURES.put(CountryCode.MK, new BbanStruct(CountryCode.MK, new BbanStructEntry[]{new BbanStructEntry(BbanEntryType.BANK_CODE, CharacterType.N, 0, 3), new BbanStructEntry(BbanEntryType.ACCOUNT_NUMBER, CharacterType.C, 3, 10), new BbanStructEntry(BbanEntryType.NATIONAL_CHECK_DIGIT, CharacterType.N, 13, 2)}));
        STRUCTURES.put(CountryCode.MT, new BbanStruct(CountryCode.MT, new BbanStructEntry[]{new BbanStructEntry(BbanEntryType.BANK_CODE, CharacterType.A, 0, 4), new BbanStructEntry(BbanEntryType.BRANCH_CODE, CharacterType.N, 4, 5), new BbanStructEntry(BbanEntryType.ACCOUNT_NUMBER, CharacterType.C, 9, 18)}));
        STRUCTURES.put(CountryCode.MR, new BbanStruct(CountryCode.MR, new BbanStructEntry[]{new BbanStructEntry(BbanEntryType.BANK_CODE, CharacterType.N, 0, 5), new BbanStructEntry(BbanEntryType.BRANCH_CODE, CharacterType.N, 5, 5), new BbanStructEntry(BbanEntryType.ACCOUNT_NUMBER, CharacterType.N, 10, 11), new BbanStructEntry(BbanEntryType.NATIONAL_CHECK_DIGIT, CharacterType.N, 21, 2)}));
        STRUCTURES.put(CountryCode.MU, new BbanStruct(CountryCode.MU, new BbanStructEntry[]{new BbanStructEntry(BbanEntryType.BANK_CODE, CharacterType.C, 0, 6), new BbanStructEntry(BbanEntryType.BRANCH_CODE, CharacterType.N, 6, 2), new BbanStructEntry(BbanEntryType.ACCOUNT_NUMBER, CharacterType.C, 8, 18)}));
        STRUCTURES.put(CountryCode.MD, new BbanStruct(CountryCode.MD, new BbanStructEntry[]{new BbanStructEntry(BbanEntryType.BANK_CODE, CharacterType.C, 0, 2), new BbanStructEntry(BbanEntryType.ACCOUNT_NUMBER, CharacterType.C, 2, 18)}));
        STRUCTURES.put(CountryCode.MC, new BbanStruct(CountryCode.MC, new BbanStructEntry[]{new BbanStructEntry(BbanEntryType.BANK_CODE, CharacterType.N, 0, 5), new BbanStructEntry(BbanEntryType.BRANCH_CODE, CharacterType.N, 5, 5), new BbanStructEntry(BbanEntryType.ACCOUNT_NUMBER, CharacterType.C, 10, 11), new BbanStructEntry(BbanEntryType.NATIONAL_CHECK_DIGIT, CharacterType.N, 21, 2)}));
        STRUCTURES.put(CountryCode.ME, new BbanStruct(CountryCode.ME, new BbanStructEntry[]{new BbanStructEntry(BbanEntryType.BANK_CODE, CharacterType.N, 0, 3), new BbanStructEntry(BbanEntryType.ACCOUNT_NUMBER, CharacterType.N, 3, 13), new BbanStructEntry(BbanEntryType.NATIONAL_CHECK_DIGIT, CharacterType.N, 16, 2)}));
        STRUCTURES.put(CountryCode.NL, new BbanStruct(CountryCode.NL, new BbanStructEntry[]{new BbanStructEntry(BbanEntryType.BANK_CODE, CharacterType.A, 0, 4), new BbanStructEntry(BbanEntryType.ACCOUNT_NUMBER, CharacterType.N, 4, 10)}));
        STRUCTURES.put(CountryCode.NO, new BbanStruct(CountryCode.NO, new BbanStructEntry[]{new BbanStructEntry(BbanEntryType.BANK_CODE, CharacterType.N, 0, 4), new BbanStructEntry(BbanEntryType.ACCOUNT_NUMBER, CharacterType.N, 4, 6), new BbanStructEntry(BbanEntryType.NATIONAL_CHECK_DIGIT, CharacterType.N, 10, 1)}));
        STRUCTURES.put(CountryCode.PK, new BbanStruct(CountryCode.PK, new BbanStructEntry[]{new BbanStructEntry(BbanEntryType.BANK_CODE, CharacterType.C, 0, 4), new BbanStructEntry(BbanEntryType.ACCOUNT_NUMBER, CharacterType.N, 4, 16)}));
        STRUCTURES.put(CountryCode.PS, new BbanStruct(CountryCode.PS, new BbanStructEntry[]{new BbanStructEntry(BbanEntryType.BANK_CODE, CharacterType.A, 0, 4), new BbanStructEntry(BbanEntryType.ACCOUNT_NUMBER, CharacterType.C, 4, 21)}));
        STRUCTURES.put(CountryCode.PL, new BbanStruct(CountryCode.PL, new BbanStructEntry[]{new BbanStructEntry(BbanEntryType.BANK_CODE, CharacterType.N, 0, 3), new BbanStructEntry(BbanEntryType.BRANCH_CODE, CharacterType.N, 3, 4), new BbanStructEntry(BbanEntryType.NATIONAL_CHECK_DIGIT, CharacterType.N, 7, 1), new BbanStructEntry(BbanEntryType.ACCOUNT_NUMBER, CharacterType.N, 8, 16)}));
        STRUCTURES.put(CountryCode.PT, new BbanStruct(CountryCode.PT, new BbanStructEntry[]{new BbanStructEntry(BbanEntryType.BANK_CODE, CharacterType.N, 0, 4), new BbanStructEntry(BbanEntryType.BRANCH_CODE, CharacterType.N, 4, 4), new BbanStructEntry(BbanEntryType.ACCOUNT_NUMBER, CharacterType.N, 8, 11), new BbanStructEntry(BbanEntryType.NATIONAL_CHECK_DIGIT, CharacterType.N, 19, 2)}));
        STRUCTURES.put(CountryCode.RO, new BbanStruct(CountryCode.RO, new BbanStructEntry[]{new BbanStructEntry(BbanEntryType.BANK_CODE, CharacterType.A, 0, 4), new BbanStructEntry(BbanEntryType.ACCOUNT_NUMBER, CharacterType.C, 4, 16)}));
        STRUCTURES.put(CountryCode.QA, new BbanStruct(CountryCode.QA, new BbanStructEntry[]{new BbanStructEntry(BbanEntryType.BANK_CODE, CharacterType.A, 0, 4), new BbanStructEntry(BbanEntryType.ACCOUNT_NUMBER, CharacterType.C, 4, 21)}));
        STRUCTURES.put(CountryCode.SM, new BbanStruct(CountryCode.SM, new BbanStructEntry[]{new BbanStructEntry(BbanEntryType.NATIONAL_CHECK_DIGIT, CharacterType.A, 0, 1), new BbanStructEntry(BbanEntryType.BANK_CODE, CharacterType.N, 1, 5), new BbanStructEntry(BbanEntryType.BRANCH_CODE, CharacterType.N, 6, 5), new BbanStructEntry(BbanEntryType.ACCOUNT_NUMBER, CharacterType.C, 11, 12)}));
        STRUCTURES.put(CountryCode.SA, new BbanStruct(CountryCode.SA, new BbanStructEntry[]{new BbanStructEntry(BbanEntryType.BANK_CODE, CharacterType.N, 0, 2), new BbanStructEntry(BbanEntryType.ACCOUNT_NUMBER, CharacterType.C, 2, 18)}));
        STRUCTURES.put(CountryCode.SI, new BbanStruct(CountryCode.SI, new BbanStructEntry[]{new BbanStructEntry(BbanEntryType.BANK_CODE, CharacterType.N, 0, 2), new BbanStructEntry(BbanEntryType.BRANCH_CODE, CharacterType.N, 2, 3), new BbanStructEntry(BbanEntryType.ACCOUNT_NUMBER, CharacterType.N, 5, 8), new BbanStructEntry(BbanEntryType.NATIONAL_CHECK_DIGIT, CharacterType.N, 13, 2)}));
        STRUCTURES.put(CountryCode.RS, new BbanStruct(CountryCode.RS, new BbanStructEntry[]{new BbanStructEntry(BbanEntryType.BANK_CODE, CharacterType.N, 0, 3), new BbanStructEntry(BbanEntryType.ACCOUNT_NUMBER, CharacterType.N, 3, 13), new BbanStructEntry(BbanEntryType.NATIONAL_CHECK_DIGIT, CharacterType.N, 16, 2)}));
        STRUCTURES.put(CountryCode.SK, new BbanStruct(CountryCode.SK, new BbanStructEntry[]{new BbanStructEntry(BbanEntryType.BANK_CODE, CharacterType.N, 0, 4), new BbanStructEntry(BbanEntryType.ACCOUNT_NUMBER, CharacterType.N, 4, 16)}));
        STRUCTURES.put(CountryCode.SE, new BbanStruct(CountryCode.SE, new BbanStructEntry[]{new BbanStructEntry(BbanEntryType.BANK_CODE, CharacterType.N, 0, 3), new BbanStructEntry(BbanEntryType.ACCOUNT_NUMBER, CharacterType.N, 3, 17)}));
        STRUCTURES.put(CountryCode.CH, new BbanStruct(CountryCode.CH, new BbanStructEntry[]{new BbanStructEntry(BbanEntryType.BANK_CODE, CharacterType.N, 0, 5), new BbanStructEntry(BbanEntryType.ACCOUNT_NUMBER, CharacterType.C, 5, 12)}));
        STRUCTURES.put(CountryCode.TN, new BbanStruct(CountryCode.TN, new BbanStructEntry[]{new BbanStructEntry(BbanEntryType.BANK_CODE, CharacterType.N, 0, 2), new BbanStructEntry(BbanEntryType.BANK_CODE, CharacterType.N, 2, 3), new BbanStructEntry(BbanEntryType.ACCOUNT_NUMBER, CharacterType.C, 5, 15)}));
        STRUCTURES.put(CountryCode.TR, new BbanStruct(CountryCode.TR, new BbanStructEntry[]{new BbanStructEntry(BbanEntryType.BANK_CODE, CharacterType.N, 0, 5), new BbanStructEntry(BbanEntryType.NATIONAL_CHECK_DIGIT, CharacterType.C, 5, 1), new BbanStructEntry(BbanEntryType.ACCOUNT_NUMBER, CharacterType.C, 6, 16)}));
        STRUCTURES.put(CountryCode.GB, new BbanStruct(CountryCode.GB, new BbanStructEntry[]{new BbanStructEntry(BbanEntryType.BANK_CODE, CharacterType.A, 0, 4), new BbanStructEntry(BbanEntryType.BRANCH_CODE, CharacterType.N, 4, 6), new BbanStructEntry(BbanEntryType.ACCOUNT_NUMBER, CharacterType.N, 10, 8)}));
        STRUCTURES.put(CountryCode.AE, new BbanStruct(CountryCode.AE, new BbanStructEntry[]{new BbanStructEntry(BbanEntryType.BANK_CODE, CharacterType.N, 0, 3), new BbanStructEntry(BbanEntryType.ACCOUNT_NUMBER, CharacterType.C, 3, 16)}));
        STRUCTURES.put(CountryCode.UA, new BbanStruct(CountryCode.UA, new BbanStructEntry[]{new BbanStructEntry(BbanEntryType.BANK_CODE, CharacterType.C, 0, 6), new BbanStructEntry(BbanEntryType.ACCOUNT_NUMBER, CharacterType.C, 6, 25)}));
        STRUCTURES.put(CountryCode.VG, new BbanStruct(CountryCode.VG, new BbanStructEntry[]{new BbanStructEntry(BbanEntryType.BANK_CODE, CharacterType.C, 0, 4), new BbanStructEntry(BbanEntryType.ACCOUNT_NUMBER, CharacterType.N, 4, 16)}));
        STRUCTURES.put(CountryCode.TL, new BbanStruct(CountryCode.TL, new BbanStructEntry[]{new BbanStructEntry(BbanEntryType.BANK_CODE, CharacterType.N, 0, 3), new BbanStructEntry(BbanEntryType.ACCOUNT_NUMBER, CharacterType.N, 3, 14), new BbanStructEntry(BbanEntryType.NATIONAL_CHECK_DIGIT, CharacterType.N, 17, 2)}));
        STRUCTURES.put(CountryCode.XK, new BbanStruct(CountryCode.XK, new BbanStructEntry[]{new BbanStructEntry(BbanEntryType.BANK_CODE, CharacterType.N, 0, 2), new BbanStructEntry(BbanEntryType.BRANCH_CODE, CharacterType.N, 2, 2), new BbanStructEntry(BbanEntryType.ACCOUNT_NUMBER, CharacterType.N, 4, 10), new BbanStructEntry(BbanEntryType.NATIONAL_CHECK_DIGIT, CharacterType.N, 14, 2)}));
        STRUCTURES.put(CountryCode.ST, new BbanStruct(CountryCode.ST, new BbanStructEntry[]{new BbanStructEntry(BbanEntryType.NATIONAL_CHECK_DIGIT, CharacterType.N, 0, 2), new BbanStructEntry(BbanEntryType.BANK_CODE, CharacterType.N, 2, 4), new BbanStructEntry(BbanEntryType.BRANCH_CODE, CharacterType.N, 6, 4), new BbanStructEntry(BbanEntryType.ACCOUNT_NUMBER, CharacterType.N, 10, 13)}));
        STRUCTURES.put(CountryCode.SC, new BbanStruct(CountryCode.SC, new BbanStructEntry[]{new BbanStructEntry(BbanEntryType.NATIONAL_CHECK_DIGIT, CharacterType.N, 0, 2), new BbanStructEntry(BbanEntryType.BANK_CODE, CharacterType.A, 2, 4), new BbanStructEntry(BbanEntryType.BRANCH_CODE, CharacterType.N, 6, 4), new BbanStructEntry(BbanEntryType.ACCOUNT_NUMBER, CharacterType.N, 10, 16)}));
        STRUCTURES.put(CountryCode.SV, new BbanStruct(CountryCode.SV, new BbanStructEntry[]{new BbanStructEntry(BbanEntryType.BANK_CODE, CharacterType.A, 0, 4), new BbanStructEntry(BbanEntryType.ACCOUNT_NUMBER, CharacterType.N, 4, 20)}));
        NATIONAL_CHECK_DIGITS = new EnumMap(CountryCode.class);
        NATIONAL_CHECK_DIGITS.put(CountryCode.FR, new FrenchCheckDigit());
    }
}
