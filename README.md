
# Codebase Atom

Project pembelajaran untuk pemula yang ingin memahami dasar-dasar pemrograman dengan .NET, C#, dan ASP.NET (Blazor). Project ini dibuat sebagai contoh aplikasi web berbasis Blazor dengan fitur sederhana: manajemen proyek, work item, upload/download dokumen, statistik/diagram, dan autentikasi menggunakan ASP.NET Identity.

Proyek ini cocok untuk dipelajari oleh yang baru mulai belajar .NET karena memisahkan concern menjadi beberapa lapisan (UI / Logics / Infrastructure) dan menggunakan library populer (MudBlazor, EF Core, Serilog, dsb.).

## Teknologi utama

- .NET 10 (TargetFramework: net10.0)
- C# (modern features / file-scoped namespaces, source generators style partials)
- Blazor (Razor Components, server-side interactive components)
- Entity Framework Core (SQL Server)
- ASP.NET Core Identity
- MudBlazor (UI component library)
- Blazor-ApexCharts (grafik)
- Serilog (logging)

## Fitur yang ada

- Autentikasi & seed user / roles (Identity + seeders)
- Manajemen Projects & Work Items (CRUD lewat "Logics")
- Upload / download dokumen
- Statistik sederhana dengan grafik
- Struktur komponen UI yang memakai MudBlazor

## Persyaratan

- .NET 10 SDK terinstal (dotnet 10.x)
- SQL Server / LocalDB untuk database (atau jalankan SQL Server di Docker)
- Visual Studio (atau VS Code + C# extension) direkomendasikan untuk eksplorasi kode
- (Opsional) dotnet-ef jika ingin menjalankan migrasi manual: `dotnet tool install --global dotnet-ef`

## Quick start (pengembangan)

1. Clone repository ini.
2. Copy konfigurasi contoh dan sesuaikan connection string:

   - Di macOS/Linux (bash):

     ```bash
     cp src/CodebaseAtom.WebUI/appsettings.Example.json src/CodebaseAtom.WebUI/appsettings.Development.json
     ```

   - Di PowerShell (Windows):

     ```powershell
     Copy-Item .\src\CodebaseAtom.WebUI\appsettings.Example.json .\src\CodebaseAtom.WebUI\appsettings.Development.json
     ```

3. Buka `src/CodebaseAtom.WebUI/appsettings.Development.json` dan atur:

   - `Database.ConnectionString` — contoh LocalDB (Windows):

     ```text
     Server=(LocalDB)\MSSQLLocalDB;Database=CodebaseAtom;Trusted_Connection=True;
     ```

   - Atau gunakan SQL Server di Docker (cross-platform):

     ```bash
     docker run -e "ACCEPT_EULA=Y" -e "SA_PASSWORD=Your_password123" -p 1433:1433 --name codebaseatom-mssql -d mcr.microsoft.com/mssql/server:2022-latest
     ```

     Connection string contoh untuk Docker:

     ```text
     Server=localhost,1433;Database=CodebaseAtom;User Id=sa;Password=Your_password123;TrustServerCertificate=True;
     ```

   - `Identity.DefaultPasswordForInitialUsers` — password default untuk user yang di-seed (contoh: `Password@123`).

4. Jalankan aplikasi (dari root repo atau mana pun):

   ```bash
   dotnet run --project src/CodebaseAtom.WebUI
   ```

   Aplikasi akan menjalankan migrasi database dan men-seed data awal otomatis pada startup (InitializeIdentityDatabase dan InitializeDatabase dipanggil di Program.cs).

5. Buka browser ke alamat yang dicetak di konsol (umumnya https://localhost:5001 atau alamat yang disediakan oleh Kestrel).

6. Login dengan user yang di-seed:

   - Username: `admin`
   - Password: nilai dari `Identity:DefaultPasswordForInitialUsers` di `appsettings.Development.json` (default pada contoh: `Password@123`).

## Migrasi manual (opsional)

Jika ingin menjalankan migrasi EF Core secara manual (butuh dotnet-ef):

```bash
# Tambah migration (jika membuat perubahan model)
dotnet ef migrations add M001_InitialSchema --context DatabaseService --output-dir Infrastructure/Database/Migrations

# Update database
dotnet ef database update --context DatabaseService

# Untuk identity context
dotnet ef migrations add M001_InitialSchema --context IdentityDatabaseContext --output-dir Infrastructure/Identity/Database/Migrations
dotnet ef database update --context IdentityDatabaseContext
```

Script contoh juga tersedia di folder migrations (`Scripts/DotnetEf.txt`).

## Struktur proyek (ringkasan)

- src/CodebaseAtom.WebUI
  - Components/ — komponen Blazor yang dapat dipakai ulang (Common, Features, Layouts)
  - Logics/ — lapisan logika aplikasi (pattern input/output/logic dipakai di banyak fitur)
  - Infrastructure/ — konfigurasi infra: Database (EF Core), Identity, FileStorage, Logging (Serilog), Options
  - Program.cs / Configure* files — entry point dan konfigurasi dependency injection
  - appsettings*.json — konfigurasi contoh

## Untuk dipelajari / eksperimen

1. Mulai dari `Program.cs` dan `ConfigureInfrastructure.cs` untuk memahami bagaimana service di-wire up.
2. Baca `ConfigureIdentity.cs` dan folder `Infrastructure/Identity` untuk melihat integrasi Identity + EF Core + seeders.
3. Telusuri `Logics/` untuk pola pemisahan Input / Logic / Output — cocok untuk belajar arsitektur sederhana.
4. Lihat `Components/Features` untuk contoh pembuatan UI menggunakan MudBlazor dan cara membuat komponen yang reusable.
5. Coba ganti provider database ke SQLite untuk pengembangan lokal tanpa instalasi SQL Server (eksperimen ini bagus untuk pemula).

## Catatan & troubleshooting

- Project di-set untuk strict build (analyzers + TreatWarningsAsErrors = true). Jika build gagal karena peringatan analyzers, Anda bisa sementara menonaktifkan `TreatWarningsAsErrors` di `CodebaseAtom.WebUI.csproj` saat belajar.
- LocalDB hanya tersedia di Windows. Jika Anda bekerja di macOS/Linux gunakan Docker SQL Server atau ubah ke SQLite.
- Jika port HTTPS sudah dipakai, perhatikan alamat yang dicetak saat `dotnet run`.

## Library & referensi

- MudBlazor — https://mudblazor.com
- Blazor-ApexCharts — https://github.com/charlielito/blazor-apexcharts
- Serilog — https://serilog.net
- EF Core — https://docs.microsoft.com/ef/core

## Kontribusi

Silakan buka issue atau pull request jika ingin menambahkan fitur pembelajaran, memperbaiki penjelasan, atau menyediakan script setup yang lebih mudah (mis. Docker-compose untuk SQL Server + appsettings otomatis).

## Lisensi

Proyek ini dilisensikan di bawah MIT License. Lihat file `LICENSE` di root repository untuk teks lengkap lisensi.

Copyright (c) 2026 Vioren Informatika Teknologi
