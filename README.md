@Override
protected void configure(HttpSecurity http) throws Exception {
    http
        .csrf().disable()
        .authorizeRequests()
            .antMatchers(HttpMethod.OPTIONS, "/**").permitAll() // Allow all OPTIONS requests
            .anyRequest().authenticated() // Secure other endpoints
        .and()
        .httpBasic();
}