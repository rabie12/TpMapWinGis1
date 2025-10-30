{company && (
  <div className="result">
    <h2>Company Information</h2>

    <p><strong>Identifier:</strong> {company.identifier}</p>
    <p><strong>Legal Name:</strong> {company.legalName}</p>
    <p><strong>Status:</strong> {company.status}</p>
    <p><strong>Country:</strong> {company.country}</p>
    <p><strong>Legal Form:</strong> {company.legalForm}</p>
    <p><strong>Capital:</strong> {company.capital}</p>
    <p><strong>Activity Code:</strong> {company.activityCode}</p>

    {company.address && (
      <>
        <h3>Address</h3>
        <p>{company.address.addressLine1}</p>
        <p>{company.address.zipCode} {company.address.city}</p>
        <p>{company.address.country}</p>
      </>
    )}

    {company.representatives?.length > 0 && (
      <>
        <h3>Representatives</h3>
        <ul>
          {company.representatives.map((rep, index) => (
            <li key={index}>
              {rep.role} – {rep.naturalPerson?.firstName} {rep.naturalPerson?.lastName}
            </li>
          ))}
        </ul>
      </>
    )}

    {company.documents?.length > 0 && (
      <>
        <h3>Documents</h3>
        <ul>
          {company.documents.map((doc, index) => (
            <li key={index}>
              {doc.type} - {doc.name} ({new Date(doc.creationDate).toLocaleDateString()})
            </li>
          ))}
        </ul>
      </>
    )}
  </div>
)}