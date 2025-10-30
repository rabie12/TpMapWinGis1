Ah! Thanks for sharing the request headers — this confirms exactly what’s happening.

From your network trace:

access-control-request-method -> GET
access-control-request-headers -> api-key
origin -> http://localhost:3000

This is a CORS preflight OPTIONS request, not your actual GET request.

✅ Why Api-Key is null
	•	The browser first sends an OPTIONS request to check if it’s allowed to send a custom header (Api-Key).
	•	Your Spring filter runs on this OPTIONS request and tries to read the header, but preflight requests don’t include your custom header — that’s why apiKey is null.

🔧 How to fix it
	1.	Allow OPTIONS to pass through your filter without checking the API key:

@Override
protected boolean shouldNotFilter(HttpServletRequest request) {
    String path = request.getRequestURI();
    return "OPTIONS".equalsIgnoreCase(request.getMethod()) ||
           path.startsWith("/auth/appClient") ||
           path.startsWith("/actuator/health") ||
           path.startsWith("/swagger") ||
           path.startsWith("/swagger-ui") ||
           path.startsWith("/health");
}

	2.	Enable CORS to allow the browser to send Api-Key in the actual GET request:

@Configuration
public class CorsConfig {
    @Bean
    public WebMvcConfigurer corsConfigurer() {
        return new WebMvcConfigurer() {
            @Override
            public void addCorsMappings(CorsRegistry registry) {
                registry.addMapping("/**")
                        .allowedOrigins("http://localhost:3000")
                        .allowedMethods("GET", "POST", "PUT", "DELETE", "OPTIONS")
                        .allowedHeaders("Api-Key", "Content-Type", "Accept")
                        .exposedHeaders("Api-Key")
                        .allowCredentials(true);
            }
        };
    }
}

	3.	Keep your filter as-is for GET/POST — it will now see the Api-Key in the actual GET request that comes after the OPTIONS preflight.

⸻

After these changes:
	•	Browser sends OPTIONS → Spring responds OK
	•	Browser sends GET with Api-Key → Spring filter reads it correctly

⸻

If you want, I can write a ready-to-use version of your filter class that works with CORS and logs the actual Api-Key for debugging.

Do you want me to do that?