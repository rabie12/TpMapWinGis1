Got it ✅ — you want to compare two BIC (SWIFT) codes, but ignore the trailing “XXX” (or any other suffix after the 8th character) when doing the match.

So for example:
	•	SPKHDE2HXXX and SPKHDE2H → ✅ should match
	•	SPKHDE2 and SPKHDE2H → ❌ should not match

Here’s a clean way to express that in Java:

// Normalize both BICs by trimming "XXX" and comparing the 8 or 11-char variants
String normalizedBicOfBa = bicOfBa.length() > 8 && bicOfBa.endsWith("XXX") 
    ? bicOfBa.substring(0, 8) 
    : bicOfBa;
String normalizedBic = bic.length() > 8 && bic.endsWith("XXX") 
    ? bic.substring(0, 8) 
    : bic;

if (!normalizedBicOfBa.startsWith(normalizedBic)) {
    // condition fails
}

✅ Explanation:
	•	A valid BIC is 8 or 11 characters.
	•	The last three chars "XXX" are a branch code and often optional.
	•	The comparison trims the "XXX" when present and then checks if the remaining core (8 chars) matches.

If you want to be stricter (i.e., compare exactly 8 chars, not just startsWith), then do:

if (!normalizedBicOfBa.equals(normalizedBic)) {
    // condition fails
}

Would you like it to handle any suffix (not just “XXX”)? For example, accept both SPKHDE2HXXX and SPKHDE2HABC as equivalent?