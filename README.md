    @Override
    protected boolean shouldNotFilter(HttpServletRequest request) throws ServletException {
        var path = request.getRequestURI();
        return path.startsWith("/auth/appClient") || path.startsWith("/actuator/health") || path.startsWith("/swagger") || path.startsWith("/swagger-ui")|| path.startsWith("/health");
    }

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

}
this is my filter the api-key is null can adjust it and then catch th exact objet and mapp it in react in similar object
