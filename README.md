UPDATE symwis.bank_info AS bi
JOIN symwis.bank_agency_info AS bai 
  ON bai.bank_info_id = bi.id
SET bi.country_iso_2 = bai.country_iso_2;