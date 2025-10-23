INSERT INTO external_bank_data.bank_info (id, address1, bic, can_dob2b_sdd, can_do_core_sdd, can_do_sct, country_iso2, created_at, institution, location, name, response_details, updated_at, search_result) VALUES(4156, 'Via A. Specchi, 16  ', 'UNCRITMMXXX', 1, 1, 1, 'IT', '2015-05-01 00:00:31.000', 'UNICREDIT SPA', 'MILANO', 'UNICREDIT SPA', NULL, '2018-04-01 08:11:42.000', NULL);

i want to update updateAT but now() respecting the format its(datetime(6)) in java its  @Column(nullable = false)
    private LocalDateTime updatedAt;
