/*===========================================================================================
 * 
 *  Author          : Arturo Salvamante
 * 
 *  File            : VCard.cs
 * 
 *  Copyright (C)   : Advanced Card System Ltd
 * 
 *  Description     : Contains Methods and Properties related vCard v3.0 (Media Type RFC 2426) 
 * 
 *  Date            : June 28, 2011
 * 
 *  Revision Traile : [Author] / [Date if modification] / [Details of Modifications done]
 * 
 * 
 * =========================================================================================*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;

namespace ACS.SmartCard.Reader.PCSC.Nfc
{
    public enum AddressType
    {
        domestic = 0x01,
        international = 0x02,
        postal = 0x04,
        parcel = 0x08,
        home = 0x10,
        work = 0x20,
        preferred = 0x40
    }

    public enum TelephoneNumberType
    {
        /// <summary>
        /// home telephone number
        /// </summary>
        home = 0x01,

        /// <summary>
        /// telephone number has voice messaging support
        /// </summary>
        msg = 0x02,

        /// <summary>
        /// Work telephone number
        /// </summary>
        work = 0x03,

        /// <summary>
        /// preferred-use telephone number
        /// </summary>
        preferred = 0x04,

        /// <summary>
        /// voice telephone number
        /// </summary>
        voice = 0x05,

        /// <summary>
        /// facsimile telephone number
        /// </summary>
        fax = 0x06,

        /// <summary>
        /// cellular telephone number
        /// </summary>
        cell = 0x07,

        /// <summary>
        /// video conferencing telephone number
        /// </summary>
        video = 0x08,

        /// <summary>
        /// paging device telephone number
        /// </summary>
        pager = 0x09,

        /// <summary>
        /// bulletin board system telephone number
        /// </summary>
        bbs = 0x0A,

        /// <summary>
        /// MODEM connected telephone number
        /// </summary>
        modem = 0x0B,

        /// <summary>
        /// car-phone telephone number
        /// </summary>
        car = 0x0C,

        /// <summary>
        /// ISDN service telephone number
        /// </summary>
        isdn = 0x0D,

        /// <summary>
        /// personal communication services 
        /// </summary>
        pcs = 0x0E
    }

    public enum EmailType
    {
        internet = 0x01,
        x400 = 0x02,
        preferred = 0x04
    }

    public class IdentificationType
    {
        const string _separator = ";";

        private string _formattedName = "";
        public string formattedName
        {
            get { return _formattedName; }
            set { _formattedName = value; }
        }

        private string _givenName = "";
        public string givenName
        {
            get { return _givenName; }
            set { _givenName = value; }
        }

        private string _additionalName = "";
        public string additionalName
        {
            get { return _additionalName; }
            set { _additionalName = value; }
        }

        private string _familyName = "";
        public string familyName
        {
            get { return _familyName; }
            set { _familyName = value; }
        }

        private string _nickName = "";
        public string nickName
        {
            get { return _nickName; }
            set { _nickName = value; }
        }

        private string _honorificPrefix = "";
        public string honorificPrefix
        {
            get { return _honorificPrefix; }
            set { _honorificPrefix = value; }
        }

        private string _honorificSuffix = "";
        public string honorificSuffix
        {
            get { return _honorificSuffix; }
            set { _honorificSuffix = value; }
        }

        private DateTime? _birthdate = null;
        public DateTime birthDate
        {
            get { return _birthdate.Value; }
            set { _birthdate = value; }
        }

        public string getFormattedString()
        {
            string formattedString = "";
            string tempString = "";

            //FORMATTED NAME
            if (formattedName.Trim() != "")
            {
                tempString = VCard.escapeString(formattedName);

                formattedString += "FN:" + tempString;
            }

            //NAME
            //Family Name, Given Name, Additional Names, Honorific Prefixes, and Honorific Suffixes.
            //The text components are separated by the SEMI-COLON character (ASCII decimal 59). Individual text
            //components can include multiple text values (e.g., multiple Additional Names) separated by the COMMA character (ASCII decimal 44).            
            //fString += "\r\nN:";

            formattedString += "N:";

            //Family name
            formattedString += familyName;

            //Given name
            formattedString += ";" + givenName;

            //Additional name
            formattedString += ";" + additionalName;

            return formattedString;
        }

        public void loadData(byte[] data)
        {
            loadData(ASCIIEncoding.ASCII.GetString(data));
        }

        public void loadData(string dataAsString)
        {
            string[] data = dataAsString.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
            string tempString = "";

            foreach (string property in data)
            {
                tempString = property;

                tempString.TrimEnd();
                tempString.TrimStart();
                tempString.Trim(new char[] { '\0', '\n', '\r' });

                //Check if formatted Name
                if(property.Substring(0,2).ToLower() == "fn")
                {
                    formattedName = property.Substring(3);
                    continue;
                }

                //Check if Name
                if (property.Substring(0, 2).ToLower() == "n")
                {
                    formattedName = property.Substring(2);
                    continue;
                }

            }
        }
    }

    public class OrganizationalType
    {
        private string _title = "";
        private string _organizationName = "";
        private string _role;
        private string _agent;
        private List<string> _organizatinalUnit = new List<string>();

        /// <summary>
        /// To specify the job title, functional position or function of the object the vCard represents
        /// Ex. TITLE:Director\, Research and Development
        /// </summary>
        public string title
        {
            get { return _title; }
            set { _title = value; }
        }
        
        /// <summary>
        /// The type is based on the X.520 Organization Name
        /// and Organization Unit attributes. The type value is a structured type
        /// consisting of the organization name, followed by one or more levels
        /// of organizational unit names.
        /// 
        /// Ex. ORG:ABC\, Inc.;North American Division;Marketing
        /// </summary>        
        public string organizationName
        {
            get { return _organizationName; }
            set { _organizationName = value; }
        }

        /// <summary>
        /// To specify information concerning the role, occupation, or business category of the object the vCard represents.
        /// Ex. ROLE:Programmer
        /// </summary>
        public string role
        {
            get { return _role; }
            set { _role = value; }
        }

        /// <summary>
        ///  To specify information about another person who will
        ///  act on behalf of the individual or resource associated with the vCard.
        ///  
        ///  Type value: The default is a single vcard value. It can also be reset
        ///  to either a single text or uri value. The text value can be used to
        ///  specify textual information. The uri value can be used to specify
        ///  information outside of this MIME entity.
        /// </summary>
        public string agent
        {
            get { return _agent; }
            set { _agent = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public List<string> organizationalUnit
        {
            get { return _organizatinalUnit; }
            set { _organizatinalUnit = value; }
        }

        /// <summary>
        /// Add Division
        /// </summary>
        /// <param name="unitName"></param>
        public void addOrganizationalUnit(string unitName)
        {
            _organizatinalUnit.Add(unitName);
        }

        public string getFormattedString()
        {
            string formattedString = "";

            //TITLE
            formattedString = "TITLE:" + VCard.escapeString(title);

            //ROLE
            formattedString += "\r\nROLE:" + VCard.escapeString(role);

            //ORGANIZATION
            formattedString += "\r\nORG:" + VCard.escapeString(organizationName);

            //Divisions/Organizational Unit
            for (int i = 0; i < organizationalUnit.Count; i++)
                formattedString += ";" + VCard.escapeString(organizationalUnit[i]);

            if (agent.Trim() != "")
            {
                //AGENT - Currently Single Text/URI is only supported. Single vCard will be supported later
                formattedString += "\r\nAGENT;VALUE=text:" + agent;
            }

            return formattedString;
        }

    }

    public class AddressingType
    {
        private string _poBoxNumber = "";
        private string _extendedAddress = "";
        private string _streetAddress = "";
        private string _locality = "";
        private string _region = "";
        private string _postalCode = "";
        private string _country = "";
        private string _label = "";

        private List<AddressType> _labelType = new List<AddressType>();
        private List<AddressType> _addressType = new List<AddressType>();

        public string poBoxNumber
        {
            get { return _poBoxNumber; }
            set { _poBoxNumber = value; }
        }

        public string extendedAddress
        {
            get { return _extendedAddress; }
            set { _extendedAddress = value; }
        }

        public string streetAddress
        {
            get { return _streetAddress; }
            set { _streetAddress = value; }
        }

        public string locality
        {
            get { return _locality; }
            set { _locality = value; }
        }

        public string region
        {
            get { return _region; }
            set { _region = value; }
        }

        public string postalCode
        {
            get { return _postalCode; }
            set { _postalCode = value; }
        }

        public string country
        {
            get { return _country; }
            set { _country = value; }
        }

        /// <summary>
        /// To specify the formatted text corresponding to delivery address of the object the vCard represents.
        /// 
        /// </summary>
        public string label
        {
            get { return _label; }
            set { _label = value; }
        }

        /// <summary>
        /// The TYPE parameter values can include "dom" to indicate a domestic delivery address; "intl" to indicate an
        /// international delivery address; "postal" to indicate a postal delivery address; "parcel" to indicate a parcel delivery address;
        /// "home" to indicate a delivery address for a residence; "work" to indicate delivery address for a place of work; and "pref" to indicate
        /// the preferred delivery address when more than one address is specified.
        /// 
        /// Value: AddressType 
        /// You can ORed AddressType to specify mutiple value
        /// </summary>
        public List<AddressType> labelType
        {
            get { return _labelType; }
            set { _labelType = value; }
        }

        /// <summary>
        /// The TYPE parameter values can include "dom" to indicate a domestic delivery address; "intl" to indicate an
        /// international delivery address; "postal" to indicate a postal delivery address; "parcel" to indicate a parcel delivery address;
        /// "home" to indicate a delivery address for a residence; "work" to indicate delivery address for a place of work; and "pref" to indicate
        /// the preferred delivery address when more than one address is specified.
        /// 
        /// Value: AddressType 
        /// You can ORed AddressType to specify mutiple value
        /// </summary>
        public List<AddressType> addressType
        {
            get { return _addressType; }
            set { _addressType = value; }
        }

        public void addLabelType(AddressType addType)
        {
            _labelType.Add(addType);
        }

        public void addAddressType(AddressType addType)
        {
            _addressType.Add(addType);
        }

        public string getFormattedString()
        {
            const string separator = ";";
            string formattedString = "";


            //ADDRESS
            formattedString = "ADR";

            //PO Box Number
            formattedString += poBoxNumber;

            //Extended Address
            formattedString += separator + VCard.escapeString(extendedAddress);

            //Street Address
            formattedString += separator + VCard.escapeString(streetAddress);

            //locality
            formattedString += separator + VCard.escapeString(locality);

            //Region
            formattedString += separator + VCard.escapeString(region);

            //Postal Code
            formattedString += separator + postalCode;

            //Country
            formattedString += separator + VCard.escapeString(country);



            //LABEL
            if (label != "")
            {
                formattedString += "\r\nLABEL;";

                //Address type
                if (labelType.Count > 0)
                {
                    formattedString += "TYPE=";

                    for (int i = 0; i < labelType.Count; i++)
                    {
                        formattedString += getAddressTypeAsString(labelType[i]);

                        if ((i + 1) < labelType.Count)
                            formattedString += ",";
                    }

                    formattedString += ":";
                }

                //Label 
                formattedString += VCard.escapeString(label);
            }

            return formattedString;

        }

        private string getAddressTypeAsString(AddressType addType)
        {
            switch (addType)
            {
                case AddressType.domestic: return "dom";
                case AddressType.home: return "home";
                case AddressType.international: return "intl";
                case AddressType.parcel: return "parcel";
                case AddressType.postal: return "postal";
                case AddressType.preferred: return "pref";
                case AddressType.work: return "work";
                default: return "home";
            }
        }
    }

    public class TelecommunicationType
    {
        private string _telephoneNumber = "";
        private string _emailAddress = "";
        private string _mailerName = "";

        public TelecommunicationType()
        {
            _telephoneNumberType.Add(TelephoneNumberType.voice);
            _emailType.Add(EmailType.internet);
        }

        private List<TelephoneNumberType> _telephoneNumberType = new List<TelephoneNumberType>();

        private List<EmailType> _emailType = new List<EmailType>();

        public string telephoneNumber
        {
            get { return _telephoneNumber; }
            set { _telephoneNumber = value; }
        }

        public string emailAddress
        {
            get { return _emailAddress; }
            set { _emailAddress = value; }
        }

        public string mailerName
        {
            get { return _mailerName; }
            set { _mailerName = value; }
        }

        public List<EmailType> emailType
        {
            get { return _emailType; }
            set { _emailType = value; }
        }

        public List<TelephoneNumberType> telephoneNumberType
        {
            get { return _telephoneNumberType; }
            set { _telephoneNumberType = value; }
        }

        public void addEmailType(EmailType emailType)
        {
            if (_emailType.Contains(emailType))
                return;

            _emailType.Add(emailType);
        }

        public void addTelephoneNumberType(TelephoneNumberType telType)
        {
            if (_telephoneNumberType.Contains(telType))
                return;

            _telephoneNumberType.Add(telType);
        }

        public string getFormattedString()
        {
            string formattedString = "";

            #region TELEPHONE

            if (telephoneNumber.Trim() != "")
            {
                formattedString += "TEL;";

                //Telephone Number type

                formattedString += "TYPE=";

                for (int i = 0; i < telephoneNumberType.Count; i++)
                {
                    formattedString += getTelphoneNumberTypeAsString(telephoneNumberType[i]);

                    if ((i + 1) < telephoneNumberType.Count)
                        formattedString += ",";
                }

                formattedString += ":";

                //Telephone Number
                formattedString += telephoneNumber;
            }

            #endregion

            #region EMAIL

            if (emailAddress.Trim() != "")
            {

                formattedString += "\nEMAIL;";

                //Email Type
                formattedString += "TYPE=";

                for (int i = 0; i < emailType.Count; i++)
                {
                    formattedString += getEmailTypeAsString(emailType[i]);

                    if ((i + 1) < emailType.Count)
                        formattedString += ",";
                }

                formattedString += ":";

                //Email Address
                formattedString += emailAddress;
            }

            #endregion

            #region MAILER

            if (mailerName != "")
            {
                formattedString += "\nMAILER:" + mailerName;
            }

            #endregion

            return formattedString;
        }

        private string getTelphoneNumberTypeAsString(TelephoneNumberType telphoneNumberType)
        {
            switch (telphoneNumberType)
            {
                case TelephoneNumberType.bbs: return "bss";
                case TelephoneNumberType.car: return "car";
                case TelephoneNumberType.cell: return "cell";
                case TelephoneNumberType.fax: return "fax";
                case TelephoneNumberType.home: return "home";
                case TelephoneNumberType.isdn: return "isdn";
                case TelephoneNumberType.modem: return "modem";
                case TelephoneNumberType.msg: return "msg";
                case TelephoneNumberType.pager: return "pager";
                case TelephoneNumberType.pcs: return "pcs";
                case TelephoneNumberType.preferred: return "pref";
                case TelephoneNumberType.video: return "video";
                case TelephoneNumberType.voice: return "voice";
                case TelephoneNumberType.work: return "work";
                default: return "voice";
            }
        }

        private string getEmailTypeAsString(EmailType emailType)
        {
            switch (emailType)
            {
                case EmailType.internet: return "internet";
                case EmailType.preferred: return "pref";
                case EmailType.x400: return "x400";
                default: return "internet";
            }
        }
    }

    public class ExplanatoryType
    {
        private string _url;

        public string url
        {
            get { return _url; }
            set { _url = value; }
        }

        public string getFormattedString()
        {
            string formattedString = "";

            //URL
            formattedString = "URL:" + url;

            return formattedString;
        }
    }

    class VCard
    {
        //private string firstname

        public static string escapeString(string text)
        {
            return escapeString(text, new char[] { ',', ';' });
        }

        public static string escapeString(string text, char[] characters)
        {
            string parsedString = "";
            string tempString = "";

            if (text == "")
                return text;

            if (characters == null || characters.Length < 1)
                return text;

            parsedString = text;

            for (int c = 0; c < characters.Length; c++)
            {
                tempString = parsedString;

                for (int indx = 0; indx < tempString.Length;)
                {
                    if (tempString[indx] == characters[c])
                    {
                        tempString = tempString.Insert(indx, @"\");
                        indx += 2;
                    }
                    else
                    {
                        indx++;
                    }
                }

                parsedString = tempString;
            }

            return parsedString;
        }
    }
}
