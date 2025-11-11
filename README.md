    @PostMapping(value = "/generate", produces = MediaType.APPLICATION_PDF_VALUE)
    public ResponseEntity<byte[]> generate(@RequestBody SctDebitData data) throws Exception {
        byte[] pdf = _sctDebitPdfWriter.generatePdf(data);
        HttpHeaders headers = new HttpHeaders();
        headers.setContentType(MediaType.APPLICATION_PDF);
        String filename = String.format("Avis-debit-%d.pdf", data.getOrderId());
        headers.setContentDispositionFormData(filename, filename);
        headers.setCacheControl("must-revalidate, post-check=0, pre-check=0");
        LOGGER.info("PDF generated successfully for order <{}>", data.getOrderId());
        return new ResponseEntity<>(pdf, headers, HttpStatus.OK);
    }

    i want to make validation for this inpu,
    all of them should be man,datary before calling this api
