UPDATE symwis.bank_info AS bi
JOIN symwis.country AS c 
  ON c.id = bi.country_id
SET bi.country_iso_2 = c.alpha2
WHERE bi.country_iso_2 IS NULL;