namespace inventoryApp.Models
{
    public class Gudang
    {
        public int KodeGudang { get; set; }
        public string NamaGudang { get; set; }
        public ICollection<Barang> Barangs { get; set; }
    }

}
