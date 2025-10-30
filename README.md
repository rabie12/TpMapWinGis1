import React, { useState } from "react";
import axios from "axios";
import "./App.css";

function App() {
  const [country, setCountry] = useState("FR");
  const [siren, setSiren] = useState("");
  const [company, setCompany] = useState(null);
  const [error, setError] = useState(null);
  const [loading, setLoading] = useState(false);

  const countries = [
    { code: "FR", name: "France" },
    { code: "LU", name: "Luxembourg" },
    { code: "GB", name: "United Kingdom" },
    { code: "CH", name: "Switzerland" },
  ];

  const handleSubmit = async (e) => {
    e.preventDefault();
    setLoading(true);
    setError(null);
    setCompany(null);
    const apiKey = 'd07c775f-326e-476c-affb-2bf712c87cee';
    try {
      const res = await axios.get(
        `http://localhost:8080/api/country/${country}/company/${siren}`, 
        {
          headers: {
            "Api-Key": 'd07c775f-326e-476c-affb-2bf712c87cee',
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
    <div className="App">
      <h1>OlkyRegister </h1>
      <form onSubmit={handleSubmit}>
        <div className="form-group">
          <label>Country:</label>
          <select value={country} onChange={(e) => setCountry(e.target.value)}>
            {countries.map((c) => (
              <option key={c.code} value={c.code}>
                {c.name}
              </option>
            ))}
          </select>
        </div>

        <div className="form-group">
          <label>SIREN:</label>
          <input
            type="text"
            value={siren}
            onChange={(e) => setSiren(e.target.value)}
            placeholder="Enter company number"
          />
        </div>

        <button type="submit" disabled={loading || !siren}>
          {loading ? "Loading..." : "Search"}
        </button>
      </form>

      {error && <p className="error"> {error}</p>}

      {company && (
        <div className="result">
          <h2>Company Information</h2>
          <pre>{JSON.stringify(company, null, 2)}</pre>
        </div>
      )}
    </div>
  );
}

export default App;



          <pre>{JSON.stringify(company, null, 2)}</pre>


          {JSON.stringify(company, null, 2)} mapit on reel DTO copagny and then have the ability to display onluy needed data from the object
