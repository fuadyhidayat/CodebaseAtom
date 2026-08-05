
# Codebase Atom

Repo ini adalah contoh aplikasi web berbasis Blazor yang dibuat untuk pembelajaran pemula tentang .NET, C#, ASP.NET Core (Razor/Blazor) dan pola sederhana pada aplikasi web.

Fokus utama yang bisa dipelajari dari project ini:

- Struktur aplikasi yang memisahkan lapisan Infrastructure / Logics / UI (komponen Razor)
- Penggunaan Entity Framework Core untuk persistence dan migrasi
- Integrasi ASP.NET Core Identity (user/roles + seeders)
- Pembuatan UI dengan MudBlazor dan pembuatan grafik dengan Blazor-ApexCharts
- Upload / download file sederhana dan penggunaan service DI

## Ringkasan teknis

- TargetFramework: `net10.0`
- UI: Blazor (Razor Components, interactive server mode)
- ORM: Entity Framework Core (SQL Server)
- Auth: ASP.NET Core Identity (dengan seed user/roles)
- UI library: MudBlazor
- Charting: Blazor-ApexCharts
- Logging: Serilog

## Fitur utama

- Autentikasi & autorisasi (Identity) dengan user/role yang di-seed
- CRUD Projects dan Work Items (kanban-like status)
- Upload / download dokumen (disimpan di `src/CodebaseAtom.WebUI/App_Data/files`)
- Statistik / chart untuk data project
- Contoh pola "Logics" (handler/service per fitur) untuk memisahkan business logic dari UI

## Persyaratan

- .NET 10 SDK (dotnet 10.x)
- SQL Server / LocalDB (Windows) atau jalankan SQL Server di Docker
- (Opsional) `dotnet-ef` jika ingin mengelola migrasi secara manual
- Editor: Visual Studio / VS Code + C# extension direkomendasikan

## Quick start (pengembangan)

1. Clone repository:

   ```bash
   git clone <repo-url>
   cd <repo-folder>
   ```

2. Konfigurasi environment:

   - Project menyediakan `src/CodebaseAtom.WebUI/appsettings.Example.json` sebagai contoh konfigurasi.
   - Jika belum ada, buat `src/CodebaseAtom.WebUI/appsettings.Development.json` dengan menyalin file contoh, atau langsung edit file `appsettings.Development.json` yang sudah ada.

   contoh (bash):

   ```bash
   cp src/CodebaseAtom.WebUI/appsettings.Example.json src/CodebaseAtom.WebUI/appsettings.Development.json
   ```

   contoh (PowerShell):

   ```powershell
   Copy-Item .\src\CodebaseAtom.WebUI\appsettings.Example.json .\src\CodebaseAtom.WebUI\appsettings.Development.json
   ```

   - Buka `src/CodebaseAtom.WebUI/appsettings.Development.json` dan sesuaikan `Database.ConnectionString` dan `Identity.ConnectionString`.
   - Nilai default pada contoh menggunakan LocalDB (Windows):

     ```text
     Server=(LocalDB)\MSSQLLocalDB;Database=CodebaseAtom;Trusted_Connection=True;
     ```

   - Jika menggunakan Docker (cross-platform), jalankan SQL Server container:

     ```bash
     docker run -e "ACCEPT_EULA=Y" -e "SA_PASSWORD=Your_password123" -p 1433:1433 --name codebaseatom-mssql -d mcr.microsoft.com/mssql/server:2022-latest
     ```

     Contoh connection string untuk Docker:

     ```text
     Server=localhost,1433;Database=CodebaseAtom;User Id=sa;Password=Your_password123;TrustServerCertificate=True;
     ```

   - Password untuk user yang di-seed diatur pada `Identity.DefaultPasswordForInitialUsers` di `appsettings.Development.json` (contoh default: `Password@123`).

3. Jalankan aplikasi:

   ```bash
   dotnet run --project src/CodebaseAtom.WebUI
   ```

   Pada startup aplikasi akan menjalankan migrasi database dan men-seed data awal otomatis (Identity + data aplikasi).

4. Buka browser ke alamat yang dicetak pada console (biasanya `https://localhost:5001` atau alamat yang ditampilkan oleh Kestrel).

5. Login dengan user yang di-seed (default):

   - Username: `admin`
   - Password: cek `Identity:DefaultPasswordForInitialUsers` pada `appsettings.Development.json` (default pada contoh: `Password@123`).

## Migrasi EF Core (opsional)

Jika Anda ingin mengelola migrasi secara manual gunakan `dotnet-ef` (tool global) dan jalankan dari folder project atau sertakan `--project`/`--startup-project` sesuai kebutuhan.

Contoh (menambahkan migration untuk DatabaseService):

```bash
dotnet ef migrations add M001_InitialSchema --project src/CodebaseAtom.WebUI --context DatabaseService --output-dir Infrastructure/Database/Migrations
dotnet ef database update --project src/CodebaseAtom.WebUI --context DatabaseService
```

Untuk Identity context gunakan `IdentityDatabaseContext` sebagai `--context`.

Catatan: project sudah otomatis menjalankan migrasi saat startup, jadi langkah manual biasanya tidak diperlukan kecuali Anda mengembangkan skema DB.

## Struktur kode (singkat)

- `src/CodebaseAtom.WebUI/Program.cs` — entry point, konfigurasi pipeline dan pemanggilan initializer
- `src/CodebaseAtom.WebUI/Infrastructure/` — konfigurasi logging, database, identity, file storage
- `src/CodebaseAtom.WebUI/Logics/` — handler / service untuk business logic (dipisahkan dari UI)
- `src/CodebaseAtom.WebUI/Components/` — komponen Blazor (Features, Common, Layouts)
- `src/CodebaseAtom.WebUI/App_Data/` — penyimpanan file yang di-upload (contoh file termasuk di repo)

## Tips untuk pemula

- Jika build gagal karena aturan analyser, periksa `CodebaseAtom.WebUI.csproj` (project di-set untuk TreatWarningsAsErrors = true). Untuk belajar Anda bisa sementara menonaktifkannya.
- LocalDB hanya tersedia di Windows. Gunakan Docker atau ganti provider ke SQLite bila diperlukan untuk pengembangan cross-platform.
- Mulai eksplorasi dari `Program.cs`, `Infrastructure/ConfigureInfrastructure.cs`, `Logics/` dan `Components/Features/Projects` untuk memahami alur data dari UI ke DB.

## Kontribusi

Silakan buka issue atau pull request untuk menambahkan materi pembelajaran, perbaikan dokumentasi, atau contoh setup (mis. docker-compose). Project ini didesain sebagai contoh pembelajaran — kami sangat menerima kontribusi yang membuat solution ini semakin beginner-friendly.

## Lisensi

Repo ini dilisensikan di bawah MIT License — lihat file `LICENSE` untuk detail.

Copyright (c) 2026 Vioren Informatika Teknologi
