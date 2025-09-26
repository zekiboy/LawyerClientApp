using System;
using System.ComponentModel.DataAnnotations;

namespace ClientApp.Models
{
    public class Case
    {
        public int Id { get; set; }

        // Foreign Key
        public int ClientId { get; set; }
        public Client? Client { get; set; }   // Acenta ile ilişki

        // Müvekkil bilgileri
        public string? Muvekkil { get; set; } //Araç Sahibi

        public string? TcVergiNo { get; set; }

        // Sigorta bilgileri
        public DateTime? SigortaBasvuruTarihi { get; set; }
        public string? SigortaSirketi { get; set; }
        public string? KaskoSirketi { get; set; }

        // Eksper bilgileri
        public string? EksperRaporu { get; set; }
        public string? EksperBilgisi { get; set; }

        // Araç ve kaza bilgileri
        public DateTime? KazaTarihi { get; set; }
        public string? MuvekkilPlaka { get; set; }
        public string? KarsiAracPlaka { get; set; }
        public string? TrafikPoliceNo { get; set; }
        public string? KaskoPoliceNo { get; set; }

        // Tahkim bilgileri
        public DateTime? TahkimBasvuruTarihi { get; set; }
        public string? TahkimBasvuruNo { get; set; }
        public string? TahkimDurumu { get; set; }

        // Dosya & hasar bilgileri
        public string? HasarDosyaNo { get; set; }
        public decimal? BakiyeHasar { get; set; }
        public decimal? SigortaKismiOdeme { get; set; }
        public DateTime? KismiOdemeTarihi { get; set; }

        // Müvekkil ödeme bilgileri
        public decimal? MuvekkileOdenen { get; set; }
        public DateTime? MuvekkileOdemeTarihi { get; set; }

        // Komisyon bilgileri
        public string? Komisyoncu { get; set; }
        public decimal? KomisyonOdeme { get; set; }

        // Masraflar
        public decimal? BasvuruUcreti { get; set; }
        public decimal? BaroPulu { get; set; }
        public decimal? BilirkişiUcreti { get; set; }
        public decimal? IslahHarci { get; set; }
        public decimal? SbmMasraf { get; set; }
        public decimal? KarsiVekalet { get; set; }
        public decimal? ItirazHarci { get; set; }
        public decimal? IcraMasraf { get; set; }
        public decimal? ToplamMasraf { get; set; }

        // Tahkim sonuçları
        public decimal? TahkimDegerKaybi { get; set; }
        public decimal? TahkimVekaletUcreti { get; set; }
        public decimal? TahkimSonrasiOdeme { get; set; }
        public DateTime? TahkimSonrasiOdemeTarihi { get; set; }

        // Bize kalan
        public decimal? BizeKalan { get; set; }


        // Yeni alanlar
        public decimal? KusurOrani { get; set; }   // % olarak
        public string? IkameAracBilgileri { get; set; }

    }
}