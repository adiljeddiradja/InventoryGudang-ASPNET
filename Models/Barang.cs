namespace inventoryApp.Models
{
    public class Barang
    {
        public int KodeBarang { get; set; }
        public string NamaBarang { get; set; }
        public decimal Harga { get; set; }
        public int Jumlah { get; set; }
        public DateTime ExpiredDate { get; set; }
        public int KodeGudang { get; set; }
        public Gudang Gudang { get; set; }
    }

}
