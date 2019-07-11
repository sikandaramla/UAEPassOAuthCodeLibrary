using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UAEPassOAuthCodeLibrary
{
    public class UAEPassUser
    {
        /// <summary>
        /// Level of assurance.
        /// </summary>
        public string userType { get; set; } //

        /// <summary>
        /// Unverified Emirates ID number.
        /// </summary>
        public string idnNotVerified { get; set; }


        /// <summary>
        /// UNIQUE PROFILE ID
        /// </summary>
        public string uuid { get; set; }

        /// <summary>
        /// Verified Emirates ID number
        /// </summary>
        public string idn { get; set; }

        /// <summary>
        /// Emirates ID card number
        /// </summary>
        public string idCardNumber { get; set; }

        /// <summary>
        /// Emirates ID card issue date
        /// </summary>
        public string idCardIssueDate { get; set; }

        /// <summary>
        /// Emirates ID card expiration date
        /// </summary>
        public string idCardExpiryDate { get; set; }

        /// <summary>
        /// Picture of the signature
        /// </summary>
        public string cardHolderSignatureImage { get; set; }

        /// <summary>
        /// Full name(English)
        /// </summary>
        public string fullnameEN { get; set; }

        /// <summary>
        /// Full name(Arabic)
        /// </summary>
        public string fullnameAR { get; set; }

        /// <summary>
        /// Given name(English)
        /// </summary>
        public string firstnameEN { get; set; }

        /// <summary>
        /// Given name(Arabic)
        /// </summary>
        public string firstnameAR { get; set; }

        /// <summary>
        /// Given name(English)
        /// </summary>
        public string lastnameEN { get; set; }

        /// <summary>
        /// Given name(Arabic)
        /// </summary>
        public string lastnameAR { get; set; }

        /// <summary>
        /// Nationality(English)
        /// </summary>
        public string nationalityEN { get; set; }

        /// <summary>
        /// Nationality(Arabic)
        /// </summary>
        public string nationalityAR { get; set; }

        /// <summary>
        /// Gender
        /// </summary>
        public string gender { get; set; }

        /// <summary>
        /// Date of birth
        /// </summary>
        public string dob { get; set; }

        /// <summary>
        /// Verified phone number
        /// </summary>
        public string mobile { get; set; }

        /// <summary>
        /// Verified email
        /// </summary>
        public string email { get; set; }

        /// <summary>
        /// Unverified email
        /// </summary>
        public string emailNotVerified { get; set; }

        /// <summary>
        /// Profile Image
        /// </summary>
        public string photo { get; set; }

        /// <summary>
        /// Marital Status
        /// </summary>
        public string maritalStatus { get; set; }

        /// <summary>
        /// Sponsor type code
        /// </summary>
        public string sponsorType { get; set; }

        /// <summary>
        /// Sponsor number
        /// </summary>
        public string sponsorNumber { get; set; }

        /// <summary>
        /// Passport number
        /// </summary>
        public string passportNumber { get; set; }

        /// <summary>
        /// Passport Country(English)
        /// </summary>
        public string passportCountryDescriptionEN { get; set; }

        /// <summary>
        /// Passport Country(Arabic)
        /// </summary>
        public string passportCountryDescriptionAR { get; set; }

        /// <summary>
        /// Passport Issue Date
        /// </summary>
        public string passportIssueDate { get; set; }

        /// <summary>
        /// Passport Expiry date
        /// </summary>
        public string passportExpiryDate { get; set; }

        /// <summary>
        /// Passport Type Code
        /// </summary>
        public string passportTypeCode { get; set; }

        /// <summary>
        /// Passport Country Code
        /// </summary>
        public string passportCountryCode { get; set; }

        /// <summary>
        /// ID Type
        /// </summary>
        public string idType { get; set; }

        /// <summary>
        /// Card Serial Number(CSN)
        /// </summary>
        public string cardSerialNumber { get; set; }

        /// <summary>
        /// Occupation Code
        /// </summary>
        public string occupationCode { get; set; }

        /// <summary>
        /// Title(English)
        /// </summary>
        public string titleEN { get; set; }

        /// <summary>
        /// Title(Arabic)
        /// </summary>
        public string titleAR { get; set; }

        /// <summary>
        /// Mother First Name(English)
        /// </summary>
        public string motherFirstNameEN { get; set; }

        /// <summary>
        /// Mother First Name(Arabic)
        /// </summary>
        public string motherFirstNameAR { get; set; }

        /// <summary>
        /// Mother fullname(English)
        /// </summary>
        public string motherFullNameEN { get; set; }

        /// <summary>
        /// Mother fullname(Arabic)
        /// </summary>
        public string motherFullNameAR { get; set; }

        /// <summary>
        /// Family ID
        /// </summary>
        public string familyNumber { get; set; }

        /// <summary>
        /// Husband emirates ID number
        /// </summary>
        public string husbandIDN { get; set; }

        /// <summary>
        /// Sponsor Name
        /// </summary>
        public string sponsorName { get; set; }

        /// <summary>
        /// Residence number
        /// </summary>
        public string residencyNumber { get; set; }

        /// <summary>
        /// Residency Type
        /// </summary>
        public string residencyType { get; set; }

        /// <summary>
        /// Residency Expiry Date
        /// </summary>
        public string residencyExpiryDate { get; set; }

        /// <summary>
        /// Place of Birth(English)
        /// </summary>
        public string placeOfBirthEN { get; set; }

        /// <summary>
        /// Place of Birth(Arabic)
        /// </summary>
        public string placeOfBirthAR { get; set; }

        /// <summary>
        /// Occupation Type(English)
        /// </summary>
        public string occupationTypeEN { get; set; }

        /// <summary>
        /// Occupation Type(Arabic)
        /// </summary>
        public string occupationTypeAR { get; set; }

        /// <summary>
        /// Occupation Field Code
        /// </summary>
        public string occupationFieldCode { get; set; }

        /// <summary>
        /// Company Name(English)
        /// </summary>
        public string companyNameEN { get; set; }

        /// <summary>
        /// Company Name(Arabic)
        /// </summary>
        public string companyNameAR { get; set; }

        /// <summary>
        /// Qualification Level Code
        /// </summary>
        public string qualificationLevelCode { get; set; }

        /// <summary>
        /// Qualification Level Description
        /// </summary>
        public string qualificationLevelDescriptionEN { get; set; }

        /// <summary>
        /// Qualification Level Description(Arabic)
        /// </summary>
        public string qualificationLevelDescriptionAR { get; set; }

        /// <summary>
        /// Degree Description(English)
        /// </summary>
        public string degreeDescriptionEN { get; set; }

        /// <summary>
        /// Degree Description(Arabic)
        /// </summary>
        public string degreeDescriptionAR { get; set; }

        /// <summary>
        /// Field of Study Code
        /// </summary>
        public string fieldOfStudyCode { get; set; }

        /// <summary>
        /// Field of Study(English)
        /// </summary>
        public string fieldOfStudyEN { get; set; }

        /// <summary>
        /// Field of Study(Arabic)
        /// </summary>
        public string fieldOfStudyAR { get; set; }

        /// <summary>
        /// Place of Study(English)
        /// </summary>
        public string placeOfStudyEN { get; set; }

        /// <summary>
        /// Place of Study(Arabic)
        /// </summary>
        public string placeOfStudyAR { get; set; }

        /// <summary>
        /// Date of Graduation
        /// </summary>
        public string dateOfGraduation { get; set; }

        /// <summary>
        /// Home - Mobile Phone
        /// </summary>
        public string homeAddressMobilePhoneNumber { get; set; }

        /// <summary>
        /// Home - Location Code
        /// </summary>
        public string homeLocationCode { get; set; }

        /// <summary>
        /// Home - Address Type Code
        /// </summary>
        public string homeAddressTypeCode { get; set; }

        /// <summary>
        /// Home - Email
        /// </summary>
        public string homeAddressEmail { get; set; }

        /// <summary>
        /// Home - Emirate Code
        /// </summary>
        public string homeAddressEmirateCode { get; set; }

        /// <summary>
        /// Home - Emirate Description(English)
        /// </summary>
        public string homeAddressEmirateDescriptionEN { get; set; }

        /// <summary>
        /// Home - Emirate Description(Arabic)
        /// </summary>
        public string homeAddressEmirateDescriptionAR { get; set; }

        /// <summary>
        /// Home - City Code
        /// </summary>
        public string homeAddressCityCode { get; set; }

        /// <summary>
        /// Home - City Description(English)
        /// </summary>
        public string homeAddressCityDescriptionEN { get; set; }

        /// <summary>
        /// Home - City Description(Arabic)
        /// </summary>
        public string homeAddressCityDescriptionAR { get; set; }

        /// <summary>
        /// Home - Street(English)
        /// </summary>
        public string homeAddressStreetEN { get; set; }

        /// <summary>
        /// Home - Street(Arabic)
        /// </summary>
        public string homeAddressStreetAR { get; set; }

        /// <summary>
        /// Home - PO Box
        /// </summary>
        public string homeAddressPOBox { get; set; }

        /// <summary>
        /// Home - Area Code
        /// </summary>
        public string homeAddressAreaCode { get; set; }

        /// <summary>
        /// Home - Area Description(English)
        /// </summary>
        public string homeAddressAreaDescriptionEN { get; set; }

        /// <summary>
        /// Home - Area Description(Arabic)
        /// </summary>
        public string homeAddressAreaDescriptionAR { get; set; }

        /// <summary>
        /// Home - Building Name(English)
        /// </summary>
        public string homeAddressBuildingNameEN { get; set; }

        /// <summary>
        /// Home - Building Name(Arabic)
        /// </summary>
        public string homeAddressBuildingNameAR { get; set; }

        /// <summary>
        /// Home - Flat Number
        /// </summary>
        public string homeAddressFlatNo { get; set; }

        /// <summary>
        /// Home - Resident Phone Number
        /// </summary>
        public string homeAddressResidentPhoneNumber { get; set; }

        /// <summary>
        /// Work - Location Code
        /// </summary>
        public string workLocationCode { get; set; }

        /// <summary>
        /// Work - Address Type Code
        /// </summary>
        public string workAddressTypeCode { get; set; }

        /// <summary>
        /// Work - Emirate Code
        /// </summary>
        public string workAddressEmirateCode { get; set; }

        /// <summary>
        /// Work - Company Name(English)
        /// </summary>
        public string workAddressCompanyNameEN { get; set; }

        /// <summary>
        /// Work - Company Name(Arabic)
        /// </summary>
        public string workAddressCompanyNameAR { get; set; }

        /// <summary>
        /// Work - Emirate Description(English)
        /// </summary>
        public string workAddressEmirateDescriptionEN { get; set; }

        /// <summary>
        /// Work - Emirate Description(Arabic)
        /// </summary>
        public string workAddressEmirateDescriptionAR { get; set; }

        /// <summary>
        /// Work - City Code
        /// </summary>
        public string workAddressCityCode { get; set; }

        /// <summary>
        /// Work - City Description(English)
        /// </summary>
        public string workAddressCityDescriptionEN { get; set; }

        /// <summary>
        /// Work - City Description(Arabic)
        /// </summary>
        public string workAddressCityDescriptionAR { get; set; }

        /// <summary>
        /// Work - Street(English)
        /// </summary>
        public string workAddressStreetEN { get; set; }

        /// <summary>
        /// Work - Street(Arabic)
        /// </summary>
        public string workAddressStreetAR { get; set; }

        /// <summary>
        /// Work - PO Box
        /// </summary>
        public string workAddressPOBox { get; set; }

        /// <summary>
        /// Work - Area Code
        /// </summary>
        public string workAddressAreaCode { get; set; }

        /// <summary>
        /// Work - Area Description(English)
        /// </summary>
        public string workAddressAreaDescriptionEN { get; set; }

        /// <summary>
        /// Work - Area Description(Arabic)
        /// </summary>
        public string workAddressAreaDescriptionAR { get; set; }

        /// <summary>
        /// Work - Building Name(English)
        /// </summary>
        public string workAddressBuildingNameEN { get; set; }

        /// <summary>
        /// Work - Building Name(Arabic)
        /// </summary>
        public string workAddressBuildingNameAR { get; set; }

        /// <summary>
        /// Work - Address land phone
        /// </summary>
        public string workAddressLandPhoneNumber { get; set; }

        /// <summary>
        /// Work - Mobile Phone Number
        /// </summary>
        public string workAddressMobilePhoneNumber { get; set; }

        /// <summary>
        /// Work - Email
        /// </summary>
        public string workAddressEmail { get; set; }

        /// <summary>
        /// ADDITIONAL DETAILS SENT BY 
        /// </summary>
        public string sub { get; set; }
        
        public string domain { get; set; }
                
        public string acr { get; set; }
        
        public List<string> amr { get; set; }
    }
}
