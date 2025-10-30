Perfect 👌 — now that we know your backend endpoint and the full JSON structure, here’s how to call it cleanly from React and display it properly.

⸻

⚙️ Your backend endpoint

GET /country/{country}/company/{id}

Example:

GET http://localhost:8080/country/FR/company/123456789

Requires header:

Api-Key: YOUR_VALID_KEY

Returns a full CompanyDTO object like the one you shared.

⸻

🧩 React front-end

Below is a complete working example (CompanyLookup.js) that:
	•	Lets the user choose a country (Connector).
	•	Lets the user type the Siren (company ID).
	•	Calls your endpoint properly with headers.
	•	Displays the formatted company response.

⸻

✅ CompanyLookup.js

import React, { useState } from "react";
import axios from "axios";

export default function CompanyLookup() {
  const [country, setCountry] = useState("FR");
  const [siren, setSiren] = useState("");
  const [company, setCompany] = useState(null);
  const [error, setError] = useState("");
  const [loading, setLoading] = useState(false);

  const countries = [
    { code: "FR", name: "France" },
    { code: "LU", name: "Luxembourg" },
    { code: "GB", name: "United Kingdom" },
    { code: "CH", name: "Switzerland" },
  ];

  const handleSubmit = async (e) => {
    e.preventDefault();
    if (!siren) {
      setError("Please enter a valid Siren/ID");
      return;
    }

    setError("");
    setCompany(null);
    setLoading(true);

    try {
      const res = await axios.get(
        `http://localhost:8080/country/${country}/company/${siren}`,
        {
          headers: {
            "Api-Key": "YOUR_VALID_API_KEY", // replace with valid API key
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

  return (
    <div style={{ maxWidth: "700px", margin: "40px auto", fontFamily: "Arial" }}>
      <h2>Company Lookup</h2>

      <form onSubmit={handleSubmit}>
        <div style={{ marginBottom: "10px" }}>
          <label>Connector (Country): </label>
          <select
            value={country}
            onChange={(e) => setCountry(e.target.value)}
            style={{ marginLeft: "10px" }}
          >
            {countries.map((c) => (
              <option key={c.code} value={c.code}>
                {c.name}
              </option>
            ))}
          </select>
        </div>

        <div style={{ marginBottom: "10px" }}>
          <label>Siren / ID: </label>
          <input
            type="text"
            value={siren}
            onChange={(e) => setSiren(e.target.value)}
            placeholder="e.g. 123456789"
            style={{ marginLeft: "10px" }}
          />
        </div>

        <button type="submit" disabled={loading}>
          {loading ? "Searching..." : "Search"}
        </button>
      </form>

      {error && <p style={{ color: "red" }}>{error}</p>}

      {company && (
        <div style={{ marginTop: "20px" }}>
          <h3>Company Details</h3>
          <p><strong>Name:</strong> {company.legalName}</p>
          <p><strong>Status:</strong> {company.status}</p>
          <p><strong>Country:</strong> {company.country}</p>
          <p><strong>RCS:</strong> {company.rcs}</p>
          <p><strong>Capital:</strong> {company.capital}</p>

          {company.address && (
            <div style={{ marginTop: "10px" }}>
              <h4>Address</h4>
              <p>
                {company.address.addressLine1} <br />
                {company.address.zipCode} {company.address.city} <br />
                {company.address.country}
              </p>
            </div>
          )}

          <h4>Representatives</h4>
          {company.representatives?.length > 0 ? (
            <ul>
              {company.representatives.map((rep) => (
                <li key={rep.id}>
                  {rep.naturalPerson.firstName} {rep.naturalPerson.lastName} — {rep.role}
                </li>
              ))}
            </ul>
          ) : (
            <p>No representatives listed</p>
          )}

          <h4>Documents</h4>
          {company.documents?.length > 0 ? (
            <ul>
              {company.documents.map((doc) => (
                <li key={doc.identifier}>
                  {doc.name} ({doc.type}) — created:{" "}
                  {new Date(doc.creationDate).toLocaleDateString()}
                </li>
              ))}
            </ul>
          ) : (
            <p>No documents</p>
          )}

          <h4>Full JSON</h4>
          <pre
            style={{
              background: "#f5f5f5",
              padding: "10px",
              borderRadius: "5px",
              fontSize: "13px",
              overflowX: "auto",
            }}
          >
            {JSON.stringify(company, null, 2)}
          </pre>
        </div>
      )}
    </div>
  );
}


⸻

🧱 3. Add to your app

In src/App.js:

import React from "react";
import CompanyLookup from "./CompanyLookup";

function App() {
  return (
    <div>
      <CompanyLookup />
    </div>
  );
}

export default App;


⸻

🧠 Notes
	•	Make sure CORS is enabled in your Spring Boot backend:

@CrossOrigin(origins = "http://localhost:3000")
@RestController
public class CompanyController { ... }


	•	Replace "YOUR_VALID_API_KEY" with an actual key from your database.

⸻

Would you like me to make it a bit more user-friendly (for example, show loading spinner and handle 400/401 errors visually)?
I can provide a polished version with Material UI if you want.