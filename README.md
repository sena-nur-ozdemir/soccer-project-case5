# ⚽ GoalZone — Premier League API Projesi | Bootcamp Case #5

Bu proje, **M&Y Yazılım Eğitim Akademi** .NET Full Stack Bootcamp kapsamında, **Murat Yücedağ** rehberliğinde geliştirilen Case #5 çalışmasıdır.

İngiltere Premier Ligi’ni baz alan bu projede; takımlar, maç sonuçları, fikstür ve istatistiklerin yönetildiği bir **ASP.NET Core Web API** geliştirilmiş ve bu API, **ASP.NET Core MVC** tarafında tüketilerek (Consume) ekrana yansıtılmıştır. 
Projenin en önemli dinamiği, veritabanında statik bir puan durumu tablosu barındırmaması ve tüm puan/averaj sıralamasını anlık olarak maç skorları üzerinden bellekte dinamik hesaplamasıdır.

---

## 🎯 Projenin Amacı

Bu proje ile aşağıdaki konularda pratik yapılmış ve yetkinlik kazanılmıştır:
* **API Geliştirme ve Tüketme:** Proje içerisinde veritabanı operasyonlarını yürüten bir backend Web API katmanı oluşturmak ve bu verileri MVC (UI) tarafında `HttpClient` ile asenkron olarak tüketmek.
* **Dinamik Hesaplama (LINQ):** Puan durumunu veritabanına kaydetmek yerine, LINQ sorgularıyla oynanan maçların skorları üzerinden canlı olarak üretmek.
* **Yapay Zeka (AI) Entegrasyonu:** Kodlama süreçlerinde ve özellikle yönetim panelinin arayüz tasarımında yapay zeka asistanlarından aktif şekilde faydalanma.

---

## 🚀 Temel Özellikler

### 🖥️ Kullanıcı Arayüzü (Vitrin)
* **Haftalık Maç Sonuçları (`Index.html`):** Seçilen haftaya göre filtrelenen maç listesi. Oynanmakta olan maçlar için canlı **"Devam Ediyor"** bildirimi.
* **Fikstür ve Form Durumu (`Fixtures.html`):** Gelecek haftanın maçları ve takımların son 5 maçtaki form grafiği (G - B - M).
* **Dinamik Puan Durumu (`Standings.html`):** * Arka planda çalışan algoritma; biten maçların skorlarını okur, galibiyete 3, beraberliğe 1 puan verir. 
  * Takımları önce puana, puan eşitliğinde averaja, averaj da eşitse atılan gole göre otomatik olarak sıralar.
* **Maç Detayı (`Match-Detail.html`):**
  * **Olay Akışı (Timeline):** Dakika bazlı goller, kartlar ve oyuncu değişiklikleri.
  * **İstatistik Paneli:** Topla oynama yüzdesi, isabetli şut, pas isabeti gibi 10 farklı metriği görsel dolgu barlarıyla (Progress Bar) kıyaslayan ekran.

### ⚙️ Admin Paneli (Yönetim)
* **Gemini AI Tasarımlı UI:** Panelin arayüzü ve veri giriş formları **Gemini AI** asistanı ile tasarlanıp projeye entegre edilmiştir.

---

## 🛠 Kullanılan Teknolojiler

* **Backend:** ASP.NET Core 8.0 Web API, ASP.NET Core 8.0 MVC, C#
* **Veritabanı:** MS SQL Server, Entity Framework Core (Code-First)
* **Veri İşleme:** LINQ, DTO (Data Transfer Object)
* **Yapay Zeka:** Gemini AI *(Admin paneli arayüz tasarımı ve kod asistanı)*
* **Frontend:** Razor Views, Bootstrap 5.3, Custom CSS, Bootstrap Icons

---

## 📸 Proje Ekran Görüntüleri

*Proje arayüzlerini detaylı incelemek için aşağıdaki başlıklara tıklayabilirsiniz:*

### 🖥️ Kullanıcı Arayüzü (Vitrin)

<details>
<summary><b>✨ Ana Sayfa: Haftalık Skorlar ve Canlı Maç Paneli</b></summary>
<br/>
<img width="3788" height="4336" alt="localhost_7298_Default_Index_ (1)" src="https://github.com/user-attachments/assets/538b5d2c-0d63-4add-9ba2-a049c71ef497" alt="Ana Sayfa" />
</details>

<details>
<summary><b>⚔️ Maç Detayı: Olay Akışı (Timeline) ve İstatistik Barları</b></summary>
<br/>
<img width="3788" height="4426" alt="localhost_7298_Fixture_GetDetail_5" src="https://github.com/user-attachments/assets/24e786f2-f5fb-481b-9424-8eee3b9099e9" alt="Maç Detayı"/>
<img width="3788" height="5270" alt="localhost_7298_Fixture_GetDetail_13" src="https://github.com/user-attachments/assets/181e6573-0c48-4345-80e1-bc00bed756f9" alt="Maç Detayı" />
</details>

<details>
<summary><b>📅 Fikstür Sayfası</b></summary>
<br/>
<img width="3788" height="4788" alt="localhost_7298_Fixture_weekNumber=34 (1)" src="https://github.com/user-attachments/assets/f8120481-044d-44a2-93de-10f4469bbbd2" alt="Fikstür"/>
<img width="3788" height="5096" alt="localhost_7298_Fixture_weekNumber=33" src="https://github.com/user-attachments/assets/b4a2f975-b71c-467d-ad2e-89c1a8385088" alt="Fikstür"/>
</details>

<details>
<summary><b>📊 Dinamik Hesaplanan Puan Durumu Tablosu</b></summary>
<br/>
<img width="3788" height="4080" alt="localhost_7298_Standings_StandingList_ (1)" src="https://github.com/user-attachments/assets/6ce20e03-99dd-48a2-8f52-eddb8a78226c" alt="Puan Durumu"/>
</details>


### ⚙️ Yönetim Paneli (Admin)

<details>
<summary><b>🛡️ Admin Paneli: Fikstür ve Maç Yönetimi</b></summary>
<br/>
<img width="3788" height="1492" alt="localhost_7298_Admin_Dashboard" src="https://github.com/user-attachments/assets/3a040234-283f-465f-a62a-0d11a31b4a39" alt="Admin Paneli Ana Sayfa"/>
</details>

<details>
<summary><b>🤖 AI Tasarımlı Veri Giriş Ekranı (Maç Olayı ve İstatistik Ekleme)</b></summary>
<br/>
<img width="1917" height="865" alt="goalzone1" src="https://github.com/user-attachments/assets/3e63355a-2014-4db6-96eb-f3d74b576cda" alt="Admin Veri Girişi"/>

<img width="1897" height="865" alt="goalzone3" src="https://github.com/user-attachments/assets/a870d423-a3c1-4879-b383-725a7f312de3" alt="Admin Veri Girişi"/>

<img width="3788" height="2394" alt="localhost_7298_Admin_CreateMatchEvent (3)" src="https://github.com/user-attachments/assets/342e1faa-47fb-4b00-a376-04fd74aa637e" alt="Admin Veri Girişi"/>

<img width="3788" height="2394" alt="localhost_7298_Admin_CreateMatchEvent (2)" src="https://github.com/user-attachments/assets/8fe43131-44d0-490f-a8a5-586dcc8be1dd" alt="Admin Veri Girişi"/>

<img width="3788" height="3712" alt="localhost_7298_Admin_CreateMatchStatistics (1)" src="https://github.com/user-attachments/assets/7ade0fc5-4ffe-4222-9eae-81f468e14735" alt="Admin Veri Girişi"/>

</details>

---

## 🎓 Eğitim

Bu proje, **M&Y Yazılım Eğitim Akademi** tarafından verilen *.NET Full Stack Bootcamp* kapsamında geliştirilmiştir. Emekleri ve anlatımı için **Murat Yücedağ** hocama teşekkür ederim.

<br/>

**👩‍💻 Developer:** Sena Nur Özdemir — [GitHub Profilim](https://github.com/sena-nur-ozdemir)
