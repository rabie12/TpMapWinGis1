Excellent — you’re almost there 👏

Let’s fix two things cleanly:
	1.	✅ Ensure your backend filter correctly reads the Api-Key header.
	2.	✅ Make your React frontend send it properly and map the full object returned by your API.

⸻

🧩 1. Fix your backend filter (Spring Boot)

Here’s a corrected version of your filter that safely handles missing headers, logs them for debugging, and prevents null issues:

@Slf4j
@Component
public class ApiKeyFilter extends OncePerRequestFilter {

    private final AuthenticationHelper authenticationHelper;

    public ApiKeyFilter(AuthenticationHelper authenticationHelper) {
        this.authenticationHelper = authenticationHelper;
    }

    @Override
    protected boolean shouldNotFilter(HttpServletRequest request) throws ServletException {
        String path = request.getRequestURI();
        return path.startsWith("/auth/appClient")
                || path.startsWith("/actuator/health")
                || path.startsWith("/swagger")
                || path.startsWith("/swagger-ui")
                || path.startsWith("/health");
    }

    @Override
    protected void doFilterInternal(HttpServletRequest request, HttpServletResponse response, FilterChain filterChain)
            throws IOException, ServletException {

        String apiKey = request.getHeader("Api-Key");

        if (apiKey == null || apiKey.isBlank()) {
            log.warn("Missing Api-Key header for request: {}", request.getRequestURI());
            response.sendError(HttpStatus.BAD_REQUEST.value(), "Missing Api-Key header");
            return;
        }

        boolean isApiKeyValid = authenticationHelper.validateApiKey(apiKey);
        if (!isApiKeyValid) {
            log.warn("Invalid Api-Key provided: {}", apiKey);
            response.sendError(HttpStatus.UNAUTHORIZED.value(), "Invalid API Key");
            return;
        }

        var authenticationToken = new UsernamePasswordAuthenticationToken(apiKey, null, Collections.emptyList());
        SecurityContextHolder.getContext().setAuthentication(authenticationToken);
        filterChain.doFilter(request, response);
    }
}

✅ This ensures:
	•	You get a clear log if the header is missing.
	•	Clients receive a clear 400 error (Missing Api-Key).
	•	Authentication context is only set if valid.

⸻

🧩 2. React frontend — sending the Api-Key and mapping response

Now in your CompanyLookup.js, modify your Axios call like this:

const handleSubmit = async (e) => {
  e.preventDefault();
  setError('');
  setResponse(null);

  try {
    const res = await axios.post(
      'http://localhost:8080/api/company/getDataForCountry',
      { siren, country },
      {
        headers: {
          'Api-Key': 'YOUR_VALID_API_KEY', // 🔑 Replace with a valid key
          'Content-Type': 'application/json',
        },
      }
    );

    setResponse(res.data);
  } catch (err) {
    console.error(err);
    setError(err.response?.data?.message || 'Request failed');
  }
};


⸻

🧱 3. Mapping the response in React

Since your response object is nested and structured, you can display it neatly using JSON formatting first:

{response && (
  <div style={{ marginTop: '20px' }}>
    <h3>API Response</h3>
    <pre>{JSON.stringify(response, null, 2)}</pre>
  </div>
)}

Once confirmed, you can map specific fields like:

{response && (
  <div>
    <h3>{response.legalName}</h3>
    <p><b>SIREN:</b> {response.identifier}</p>
    <p><b>Country:</b> {response.country}</p>
    <p><b>Status:</b> {response.status}</p>

    <h4>Address</h4>
    <p>{response.address?.addressLine1}, {response.address?.city}</p>
  </div>
)}


⸻

✅ Summary

Layer	Purpose	Key Action
Backend filter	Secure API with API key	Log, validate, and handle missing header
Frontend React	Send API key & data	Add Api-Key in Axios headers
UI Mapping	Display company object	Use JSON.stringify or mapped fields


⸻

Would you like me to extend this React app to handle loading states and error messages cleanly (with Bootstrap or Material UI)? It’d make it production-ready.