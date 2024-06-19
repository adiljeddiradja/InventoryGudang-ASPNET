*1. Jika di dalam suatu Gudang Super Market, pegawai A ingin melakukan pengecekan barang yang 
sudah kadaluarsa, berikan contoh Solusi cara menanggulangi dan mengantisipasi dalam pengecekan 
barang yang kadaluarsa*

Jawab :
1. Pastikan setiap kali barang baru masuk ke gudang, diberi label dengan tanggal kadaluarsa yang jelas.
2. Inputkan semua detail barang beserta tanggal kadaluarsa ke dalam sistem inventaris.
3. Susun barang di rak menggunakan metode FIFO (First In First Out), di mana barang yang sudah lama masuk diletakkan di depan.
4. Lakukan pemeriksaan rutin setiap minggu di rak gudang untuk mencari barang yang mendekati atau sudah melewati tanggal kadaluarsa.
5. Catat semua barang yang mendekati tanggal kadaluarsa dalam daftar pemeriksaan.
6. Beri tanda khusus pada barang-barang yang mendekati tanggal kadaluarsa untuk mempermudah pengidentifikasian.
7. Pindahkan barang yang hampir kadaluarsa ke rak promosi untuk dijual dengan diskon.
8. Gunakan sistem inventaris yang dilengkapi dengan notifikasi otomatis untuk memberitahu jika barang mendekati tanggal kadaluarsa.
9. Lakukan pembuangan barang yang sudah kadaluarsa sesuai dengan prosedur yang telah ditetapkan.
10. Catat setiap barang yang dibuang untuk keperluan pelaporan dan audit.

Jawab :

*2. - Buat 2 Table (Table Gudang & Table Barang), berikan foreignKey dan Index  - Buat Store Prosedure tampilkan list data Kode Gudang, Nama Gudang, Kode Barang, Nama Barang, 
Harga Barang, Jumlah Barang, Expired Barang mengunakan Dynamic Query dan Paging - Buat Trigger ketika Input Barang di salah satu gudang muncul kan barang yang kadaluarsa*
##Buat Tabel Gudang dan Barang


 *Tabel Gudang*
CREATE TABLE Gudang (
    KodeGudang INT PRIMARY KEY,
    NamaGudang NVARCHAR(100)
);

*Tabel Barang*

CREATE TABLE Barang (
    KodeBarang INT PRIMARY KEY,
    NamaBarang NVARCHAR(100),
    HargaBarang DECIMAL(18, 2),
    JumlahBarang INT,
    ExpiredBarang DATE,
    KodeGudang INT,
    FOREIGN KEY (KodeGudang) REFERENCES Gudang(KodeGudang)
);


CREATE INDEX IDX_Barang_KodeGudang ON Barang(KodeGudang);

##Buat Stored Procedure dengan Dynamic Query dan Paging


CREATE PROCEDURE sp_GetBarangWithPaging
    @PageNumber INT,
    @PageSize INT
AS
BEGIN
    DECLARE @Offset INT
    SET @Offset = (@PageNumber - 1) * @PageSize

    DECLARE @SQL NVARCHAR(MAX)
    SET @SQL = '
        SELECT 
            g.KodeGudang,
            g.NamaGudang,
            b.KodeBarang,
            b.NamaBarang,
            b.HargaBarang,
            b.JumlahBarang,
            b.ExpiredBarang
        FROM 
            Gudang g
        INNER JOIN 
            Barang b ON g.KodeGudang = b.KodeGudang
        ORDER BY 
            g.KodeGudang
        OFFSET ' + CAST(@Offset AS NVARCHAR) + ' ROWS
        FETCH NEXT ' + CAST(@PageSize AS NVARCHAR) + ' ROWS ONLY;'
    
    EXEC sp_executesql @SQL
END


##Buat Trigger untuk Menampilkan Barang yang Kadaluarsa


CREATE TRIGGER trg_CheckExpiredBarang
ON Barang
AFTER INSERT
AS
BEGIN
    DECLARE @CurrentDate DATE
    SET @CurrentDate = GETDATE()

    SELECT 
        i.KodeBarang,
        i.NamaBarang,
        i.HargaBarang,
        i.JumlahBarang,
        i.ExpiredBarang
    FROM 
        Inserted i
    WHERE 
        i.ExpiredBarang < @CurrentDate
END
*3.Ada di project*
