INSERT INTO symwis.bank_agency_info (id, iban_country_id, bank_info_id, bank_code, branch_code, country_iso_2) VALUES(364802, 457, 38937, 'CHGL', NULL, 'VG');
INSERT INTO symwis.bank_info (id, bic, name, institution, address1, location, can_do_sct, can_do_core_sdd, can_do_b2b_sdd, created_at, updated_at, country_id, country_iso_2) VALUES(100, 'SPKHDE2HXXX', 'Sparkasse Hannover', 'SPARKASSE HANNOVER', 'AEGIDIENTORPLATZ 1', 'HANNOVER', 1, 1, 1, '2015-04-01 16:56:07.000', '2015-10-01 09:28:22.000', 9, NULL);

i have this table and i want to update all country_iso_2 in bank_info from bank_agency_info get the info
