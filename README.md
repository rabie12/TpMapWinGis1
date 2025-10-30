Got it 👍 — you want to see how to build the React UI side (frontend) for your /country/{country}/company/{id} API, including sending the API key and displaying the structured company data.

Here’s the full working React app example 👇

⸻

🧱 App.js

import React, { useState } from "react";
import axios from "axios";

function App() {
  const [country, setCountry] = useState("FR");
  const [siren, setSiren] = useState("");
  const [company, setCompany] = useState(null);
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState("");

  const apiKey = "d07c775f-326e-476c-affb-2bf712c87cee";

  const handleSubmit = async (e) => {
    e.preventDefault();
    setError("");
    setCompany(null);
    setLoading(true);

    try {
      const res = await axios.get(
        `http://localhost:8080/country/${country}/company/${siren}`,
        {
          headers: {
            "Api-Key": apiKey,
          },
        }
      );
      setCompany(res.data);
    } catch (err) {
      console.error(err);
      setError(
        err.response?.data?.message ||
          `Error: ${err.response?.status || "Network error"}`
      );
    } finally {
      setLoading(false);
    }
  };

  return (
    <div style={{ maxWidth: 900, margin: "40px auto", fontFamily: "Arial" }}>
      <h2>Olky Register Company Lookup</h2>
      <form onSubmit={handleSubmit} style={{ marginBottom: 20 }}>
        <div style={{ marginBottom: 10 }}>
          <label>
            Country:
            <select
              value={country}
              onChange={(e) => setCountry(e.target.value)}
              style={{ marginLeft: 10 }}
            >
              <option value="FR">France (FR)</option>
              <option value="LU">Luxembourg (LU)</option>
              <option value="GB">United Kingdom (GB)</option>
              <option value="CH">Switzerland (CH)</option>
            </select>
          </label>
        </div>

        <div style={{ marginBottom: 10 }}>
          <label>
            Company ID / Siren:
            <input
              type="text"
              value={siren}
              onChange={(e) => setSiren(e.target.value)}
              required
              style={{ marginLeft: 10 }}
            />
          </label>
        </div>

        <button
          type="submit"
          disabled={loading}
          style={{
            padding: "6px 15px",
            cursor: "pointer",
            backgroundColor: "#007BFF",
            color: "white",
            border: "none",
            borderRadius: "5px",
          }}
        >
          {loading ? "Loading..." : "Search"}
        </button>
      </form>

      {error && <div style={{ color: "red" }}>{error}</div>}

      {company && (
        <div style={{ background: "#f4f4f4", padding: 15, borderRadius: 5 }}>
          <h3>{company.legalName || "No legal name available"}</h3>
          <p>
            <strong>Identifier:</strong> {company.identifier}
          </p>
          <p>
            <strong>Status:</strong> {company.status}
          </p>
          <p>
            <strong>Country:</strong> {company.country}
          </p>
          <p>
            <strong>Legal Form:</strong> {company.legalForm}
          </p>
          <p>
            <strong>Capital:</strong> {company.capital}
          </p>
          <p>
            <strong>Activity Code:</strong> {company.activityCode}
          </p>

          <h4>Address</h4>
          {company.address ? (
            <ul>
              <li>{company.address.addressLine1}</li>
              <li>{company.address.zipCode} {company.address.city}</li>
              <li>{company.address.country}</li>
            </ul>
          ) : (
            <p>No address data</p>
          )}

          <h4>Documents</h4>
          {company.documents?.length ? (
            <ul>
              {company.documents.map((doc, idx) => (
                <li key={idx}>
                  <strong>{doc.name}</strong> — {doc.type} ({doc.creationDate})
                </li>
              ))}
            </ul>
          ) : (
            <p>No documents</p>
          )}
        </div>
      )}
    </div>
  );
}

export default App;


⸻

⚙️ Instructions
	1.	Run your backend at http://localhost:8080
Ensure CORS allows http://localhost:3000 (see previous CorsConfig fix).
	2.	Start the React app:

npm start


	3.	Enter:
	•	Country → e.g., FR
	•	Siren → your test identifier (e.g., 123456789)
	4.	It will send:

GET http://localhost:8080/country/FR/company/123456789
Api-Key: d07c775f-326e-476c-affb-2bf712c87cee


	5.	You’ll see the structured company data rendered in the UI.

⸻

Would you like me to extend the UI to also show representatives and beneficialOwners (with sub-sections for each)?