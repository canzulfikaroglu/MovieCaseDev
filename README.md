Bu API projesi, TMDB (The Movie Database) servisinden alınan film verilerini yerel bir veritabanına kaydederek, doğrudan TMDB API'sine bağımlı kalmaksızın film bilgileri üzerinde işlem yapılmasına olanak tanır.
Böylece sistem, dış bağımlılık olmadan film listeleme, detay görüntüleme, puanlama ve önerme gibi işlevleri kendi veritabanı üzerinden sürdürebilir.

## 🚀 Teknolojiler

- ASP.NET Core 9
- Entity Framework Core
- PostgreSQL
- Auth0 (Authentication & Authorization)
- TMDB API
  
Projeyi Bilgisayarınızda çalıştırmak için:
Bu projeyi klonlayın:
   ```bash
   git clone https://github.com/kullaniciAdi/movie-api.git
   cd movie-api

Kullanılan Bağımlılıklar :
Auth0.AspNetCore.Authentication
MailKit
Microsoft.AspNetCore.Authentication.JwtBearer
Microsoft.AspNetCore.OpenApi
Microsoft.EntityFrameworkCore.Design
Microsoft.EntityFrameworkCore.Tools
MimeKit
Npgsql
Npgsql.EntityFrameworkCore.PostgreSQL
RestSharp
Swashbuckle.AspNetCore
