using System;
using System.IO;
using System.Linq;
using OpenRealEstate.Core;
using OpenRealEstate.Core.Land;
using OpenRealEstate.Core.Rental;
using OpenRealEstate.Core.Residential;
using OpenRealEstate.Core.Rural;
using OpenRealEstate.Transmorgrifiers.RealestateComAu;
using Shouldly;

namespace OpenRealEstate.Validation.Tests
{
    public abstract class TestBase
    {
        private const string SamplesDirectoryPath = "Sample Data\\";

        protected T GetListing<T>() where T : Listing
        {
            string fileName = null;
            if (typeof(T) == typeof(ResidentialListing))
            {
                fileName = "Residential\\REA-Residential-Current.xml";
            }
            else if (typeof(T) == typeof(RentalListing))
            {
                fileName = "Rental\\REA-Rental-Current.xml";
            }
            else if (typeof(T) == typeof(RuralListing))
            {
                fileName = "Rural\\REA-Rural-Current.xml";
            }
            else if (typeof(T) == typeof(LandListing))
            {
                fileName = "Land\\REA-Land-Current.xml";
            }

            if (string.IsNullOrWhiteSpace(fileName))
            {
                throw new Exception("No valid type provided. Must be a 'Listing' type.");
            }

            return GetListing<T>(fileName);
        }

        protected T GetListing<T>(string fileName) where T : Listing
        {
            fileName.ShouldNotBeNullOrEmpty();

            var reaXml = File.ReadAllText($"{SamplesDirectoryPath}{fileName}");
            var reaXmlTransmorgrifier = new ReaXmlTransmorgrifier();
            return (T) reaXmlTransmorgrifier.Parse(reaXml)
                                            .Listings
                                            .First()
                                            .Listing;
        }
    }
}