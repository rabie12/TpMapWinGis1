   @Override
    protected void doFilterInternal(HttpServletRequest request, HttpServletResponse response, FilterChain filterChain)
            throws IOException, ServletException {

        String apiKey = request.getHeader("Api-Key");
        boolean isApiKeyValid = authenticationHelper.validateApiKey(apiKey);
        if (!isApiKeyValid) {
            response.sendError(HttpStatus.UNAUTHORIZED.value(), "Invalid API Key");
            return;
        }
        var authenticationToken = new UsernamePasswordAuthenticationToken(apiKey, null, Collections.emptyList());
        SecurityContextHolder.getContext().setAuthentication(authenticationToken);
        filterChain.doFilter(request, response);

    }
this is my front : 
  const handleSubmit = async (e) => {
    e.preventDefault();
    setLoading(true);
    setError(null);
    setCompany(null);
    const apiKey = 'd07c775f-326e-476c-affb-2bf712c87cee';
    try {
      const res = await axios.get(
        `http://localhost:8080/country/${country}/company/${siren}`,
        {
          headers: {
            "Api-Key": `${apiKey}`,
          },
        }
      );
      setCompany(res.data);
    } catch (err) {
      console.error(err);
      setError(
        err.response?.data?.message ||
          `Request failed: ${err.response?.status || "Network error"}`
      );
    } finally {
      setLoading(false);
    }
  };
