
# Codebase Atom

Codebase Atom adalah aplikasi contoh untuk belajar .NET, C#, ASP.NET Core dan Blazor (Razor Components — interactive server render mode). Project ini dibuat sebagai bahan latihan untuk pemula yang ingin mempelajari pola aplikasi web modern: pemisahan lapisan (Infrastructure / Logics / UI), Entity Framework Core, ASP.NET Core Identity, dan pembuatan UI dengan MudBlazor.

## Ringkasan fitur utama

- Proses bisnis aplikasi: Mengelola Projects tang di dalamnya terdapat Work Items dan Documents.
- Autentikasi & autorisasi menggunakan ASP.NET Core Identity (user & role disediakan secara otomatis saat startup).
- Contoh penggunaan Entity Framework Core (migrasi & seeding) dan pola pemisahan business logic di folder `Logics/`.
- UI menggunakan MudBlazor dan charting dengan Blazor-ApexCharts.

## Teknologi yang digunakan

- Target framework: .NET 10 (`net10.0`)
- Database: Microsoft SQL Server
- Identity: ASP.NET Core Identity
- ORM: Entity Framework Core
- UI framework: Blazor (Razor Components, interactive server render mode)
- UI library: MudBlazor
- Charting library: Blazor-ApexCharts

## Persyaratan

- .NET 10 SDK (`dotnet --version` harus menampilkan versi 10.x).
- SQL Server Express LocalDB (atau SQL Server edisi lainnya) di Windows atau jalankan SQL Server di Docker.
- (Opsional) `dotnet-ef` jika Anda ingin mengelola migrasi secara manual.
- Editor yang direkomendasikan:
  - Visual Studio 2026
  - VS Code, dengan extension:
    - C# Dev Kit by Microsoft
    - Blazor Snippets Pack by Adrian Wilczyński
    - EditorConfig by EditorConfig

## Quick start

1. Clone repository:

   ```bash
   git clone <repo-url>
   cd <repo-folder>
   ```

2. Salin konfigurasi contoh dan sesuaikan environment:

   - File contoh: `src/CodebaseAtom.WebUI/appsettings.Example.json`.
   - Copy file `appsettings.Example.json` di folder `src/CodebaseAtom.WebUI` dan paste sebagai file `appsettings.Development.json` di folder yang sama.
   - Edit file `appsettings.Development.json` tersebut, ubah isi pengaturan untuk section `Database.ConnectionString` dan section `Identity.ConnectionString`.
     - Contoh default (SQL Server Express LocalDB di Windows):

     ```text
     Server=(LocalDB)\MSSQLLocalDB;Database=CodebaseAtom;Trusted_Connection=True;
     ```

     - Alternatif (SQL Server via Docker):

     ```bash
     docker run -e "ACCEPT_EULA=Y" -e "SA_PASSWORD=Your_password123" -p 1433:1433 --name codebaseatom-mssql -d mcr.microsoft.com/mssql/server:2022-latest
     ```

     Contoh connection string untuk container Docker:

     ```text
     Server=localhost,1433;Database=CodebaseAtom;User Id=sa;Password=Your_password123;TrustServerCertificate=True;
     ```

   - Password default untuk akun yang di-seed disimpan di section `Identity:DefaultPasswordForInitialUsers` pada file `appsettings.Development.json`.

3. Jalankan aplikasi:

   ```bash
   dotnet run --project src/CodebaseAtom.WebUI
   ```

   Saat startup, aplikasi akan menerapkan migrasi EF Core dan men-seed data awal (Identity + data contoh) secara otomatis.

4. Buka browser dan arahkan ke alamat yang ditampilkan di console (misalnya `https://localhost:44366`).

5. Login (contoh user seeding):

   - Username: `admin`
   - Password: lihat section `Identity:DefaultPasswordForInitialUsers` di file `appsettings.Development.json` (contoh: `Password@123`).

## Migrasi EF Core (opsional)

Project `CodebaseAtom.WebUI` sudah menjalankan `Database.Migrate()` saat startup, sehingga Anda tidak perlu menjalankan migrasi secara manual.
Jika Anda ingin mengelola migrasi sendiri, gunakan `dotnet-ef` dan jalankan per database context:

- Untuk database aplikasi (DbContext: `DatabaseContext`):

  ```bash
  dotnet ef migrations add M001_InitialSchema --project src/CodebaseAtom.WebUI --context DatabaseContext --output-dir Infrastructure/Database/Migrations
  dotnet ef database update --project src/CodebaseAtom.WebUI --context DatabaseContext
  ```

- Untuk Identity (DbContext: `IdentityDatabaseContext`):

  ```bash
  dotnet ef migrations add M001_Identity --project src/CodebaseAtom.WebUI --context IdentityDatabaseContext --output-dir Infrastructure/Identity/Database/Migrations
  dotnet ef database update --project src/CodebaseAtom.WebUI --context IdentityDatabaseContext
  ```

## Rekomendasi dalam membaca source code

- `src/CodebaseAtom.WebUI/Program.cs` — titik masuk aplikasi; lihat cara inisialisasi infrastructure, logics, dan web UI.
- `src/CodebaseAtom.WebUI/Infrastructure/` — konfigurasi database, identity, logging, dan penyimpanan file.
- `src/CodebaseAtom.WebUI/Logics/` — contoh pemisahan business logic (handler/service) yang dipanggil dari UI.
- `src/CodebaseAtom.WebUI/Components/Features/Projects` — user interface untuk mengelola Projects, Work Items, dan Documents.
- `src/CodebaseAtom.WebUI/Components/Features/Account` — user interface untuk login, lupa password, reset password, dan manajemen akun.

## Tips singkat

- Jika build gagal karena aturan analyzer, cek `CodebaseAtom.WebUI.csproj` (proyek mengaktifkan TreatWarningsAsErrors). Untuk eksplorasi awal Anda bisa menonaktifkan sementara pengaturan ini di file `.csproj`.
- SQL Server Express LocalDB hanya tersedia di Windows; gunakan Docker bila ingin pengembangan cross-platform.
- File yang diupload disimpan ke folder yang dikonfigurasi di `FileStorage:FolderPath` pada `appsettings.*.json`.

## Kontribusi

Silakan buka issue atau pull request untuk perbaikan dokumentasi, penambahan materi pembelajaran, atau contoh setup (misalnya docker-compose). Project ini dibuat sebagai bahan pembelajaran — kami sangat menyambut kontribusi yang membuatnya lebih beginner-friendly.

## Lisensi

Project ini dilisensikan di bawah MIT License — lihat file `LICENSE` untuk detail.

Copyright (c) 2026 Vioren Informatika Teknologi
