namespace SociedadesAPI.DTO
{
    public class SociedadInput
    {
        public string consecutivo { get; set; } = "";
        public string clave_casfim { get; set; } = "";
        public string sociedad { get; set; } = "";
        public string capital_neto { get; set; } = "";
        public string requerimiento_capital { get; set; } = "";
        public string nicap { get; set; } = "";
        public string categoria { get; set; } = "";
        public string federacion { get; set; } = "";
    }

    public class ResultWrapper
    {
        public List<SociedadInput> result { get; set; } = new();
    }

    public class DataWrapper
    {
        public List<ResultWrapper> data { get; set; } = new();
    }

    public class RootWrapper
    {
        public List<DataWrapper> data { get; set; } = new(); 
    }
}
