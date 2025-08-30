Bu proje, temel bir e-ticaret sipariş yönetim sistemi örneğidir. Amaç, bir e-ticaret platformunda sipariş oluşturma, listeleme, detay görüntüleme ve silme işlemlerini stok kontrolü ile birlikte sağlamaktır.

🚀 Kullanılan Teknolojiler
	•	ASP.NET Core 8 Web API – RESTful servis yapısı
	•	Entity Framework Core – ORM aracı
	•	SQLite – Dosya tabanlı veritabanı (H2 Database benzeri, kurulum gerektirmez)
	•	Swagger (Swashbuckle.AspNetCore) – API dokümantasyonu ve test aracı
	•	Dependency Injection (DI) – Servislerin yönetimi
	•	Repository/Service mantığı – İş kurallarının controller’dan ayrıştırılması

⚙️ Özellikler
	•	Yeni Sipariş Ekleme
	•	Kullanıcı sipariş verir.
	•	Stok kontrolü yapılır, ürün stoğu yeterli değilse hata döner.
	•	Sipariş ve sipariş detayları veritabanına kaydedilir.
	•	Siparişleri Listeleme
	•	Kullanıcının tüm siparişleri listelenir.
	•	Sipariş Detayını Getirme
	•	Sipariş ID’ye göre detaylı bilgi (ürünler, adet, toplam tutar) döner.
	•	Sipariş Silme
	•	Sipariş silindiğinde ürün stokları iade edilir (geri eklenir).
