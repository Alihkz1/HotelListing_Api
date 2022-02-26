using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using HotelListing_webAPI.Data;

namespace HotelListing_webAPI.Models
{

    public class CreateCountryDTO
    {
        [Required]
        [StringLength(30, ErrorMessage = " Country name is too long")]
        public string Name { get; set; }
        [Required]
        [StringLength (2  , ErrorMessage ="short countryName must be 2 characters")]
        public string ShortName { get; set; }
    }

    public class UpdateCountryDTO : CreateHotelDTO
    {
        public IList<CreateHotelDTO> Hotels { get; set; }
    }

    public class CountryDTO : CreateCountryDTO
    {
        public int Id { get; set; }
        public IList<Hotel> Hotels { get; set; }    
    }
}
 