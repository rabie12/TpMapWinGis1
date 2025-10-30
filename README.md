Perfect — you’ve done almost everything correctly.
Your React code is sending the Api-Key header properly, and your backend filter looks right.

So since it works in Bruno, but not when called from React, the only remaining culprit is CORS / preflight and how browsers treat custom headers.

Let’s fix that cleanly 👇

⸻

✅ 1. Allow Api-Key header in CORS config

In Spring Boot, by default, CORS doesn’t allow custom headers like "Api-Key" unless explicitly configured.
That means the browser silently drops it during the real request (Bruno doesn’t — that’s why Bruno works).

Add this configuration class:

import org.springframework.context.annotation.Bean;
import org.springframework.context.annotation.Configuration;
import org.springframework.web.cors.CorsConfiguration;
import org.springframework.web.cors.UrlBasedCorsConfigurationSource;
import org.springframework.web.filter.CorsFilter;

import java.util.List;

@Configuration
public class CorsConfig {

    @Bean
    public CorsFilter corsFilter() {
        CorsConfiguration config = new CorsConfiguration();
        config.setAllowedOrigins(List.of("http://localhost:3000")); // your React dev server
        config.setAllowedMethods(List.of("GET", "POST", "PUT", "DELETE", "OPTIONS"));
        config.setAllowedHeaders(List.of("Api-Key", "Content-Type", "Authorization"));
        config.setExposedHeaders(List.of("Api-Key"));
        config.setAllowCredentials(true);

        UrlBasedCorsConfigurationSource source = new UrlBasedCorsConfigurationSource();
        source.registerCorsConfiguration("/**", config);
        return new CorsFilter(source);
    }
}

This ensures the browser can legally send the Api-Key header to your backend.

⸻

✅ 2. Skip the preflight OPTIONS request in your filter

Browsers send a CORS preflight request (OPTIONS) before the actual GET.
That request does not include any headers like Api-Key, so your filter sees null.

Add this to your filter:

@Override
protected void doFilterInternal(HttpServletRequest request, HttpServletResponse response, FilterChain filterChain)
        throws IOException, ServletException {

    // Skip CORS preflight requests
    if ("OPTIONS".equalsIgnoreCase(request.getMethod())) {
        response.setStatus(HttpServletResponse.SC_OK);
        return;
    }

    String apiKey = request.getHeader("Api-Key");
    if (apiKey == null) {
        response.sendError(HttpStatus.UNAUTHORIZED.value(), "Missing API Key");
        return;
    }

    boolean isApiKeyValid = authenticationHelper.validateApiKey(apiKey);
    if (!isApiKeyValid) {
        response.sendError(HttpStatus.UNAUTHORIZED.value(), "Invalid API Key");
        return;
    }

    var authenticationToken = new UsernamePasswordAuthenticationToken(apiKey, null, Collections.emptyList());
    SecurityContextHolder.getContext().setAuthentication(authenticationToken);
    filterChain.doFilter(request, response);
}


⸻

✅ 3. Double-check React request (it’s already good)

Your React axios code is perfect:

headers: {
  "Api-Key": apiKey,
},

Just make sure your backend runs on http://localhost:8080 and your React app on http://localhost:3000.

⸻

🧠 Summary

Problem	Fix
Browser blocks custom header	Add Api-Key to allowed CORS headers
Preflight request has no headers	Skip OPTIONS requests in filter
Works in Bruno but not in React	Always caused by CORS/preflight


⸻

Once you add the CorsFilter bean and skip OPTIONS requests, you’ll see your backend correctly receives:

Api-Key : d07c775f-326e-476c-affb-2bf712c87cee

Would you like me to show how to log all headers in the filter for debugging, so you can visually confirm it’s now being received?