# Codebase Atom

Codebase Atom adalah aplikasi contoh manajemen proyek berbasis ASP.NET Core dan Blazor. Repository ini dirancang sebagai bahan belajar untuk melihat penerapan Razor Components dengan interactive server rendering, Entity Framework Core, ASP.NET Core Identity, dan MudBlazor dalam satu proyek web.

## Ringkasan fitur utama

- Proses bisnis aplikasi:
  - Mengelola Projects yang di dalamnya terdapat Work Items dan Documents.
  - Halaman profil pengguna, ganti password dan reset password.
  - Halaman dashboard yang berisi ringkasan project dan visualisasi chart.
  - Halaman *Examples* untuk melihat penggunaan komponen UI dasar dan pola MudBlazor di dalam aplikasi.
- Autentikasi & autorisasi menggunakan ASP.NET Core Identity
  - Aksi tambah, ubah, dan hapus project hanya tersedia bagi pengguna dengan role Administrator`.
- Data awal berupa user, role, project, dan work item diisi otomatis ketika aplikasi pertama kali berjalan.
- Contoh penggunaan migration dan seeding pada Entity Framework Core.
- Source code yang fokus pada business logic terdapat di folder `Logics/`.
- Komponen-komponen User Interface menggunakan MudBlazor dan charting dengan Blazor-ApexCharts.

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

## Struktur repository

```text
.
├── CodebaseAtom.slnx
├── src/
│   └── WebUI/                 # Satu proyek aplikasi web
│       ├── Components/        # Halaman dan komponen Blazor
│       ├── Domain/            # Entitas dan aturan domain dasar
│       ├── Infrastructure/    # Database, Identity, file storage, dan konfigurasi
│       ├── Logics/            # Use case / business logic per fitur
│       ├── appsettings.json
│       └── appsettings.Example.json
└── README.md
```

## Menjalankan aplikasi

1. Clone repository, lalu masuk ke direktori repository.

   ```bash
   git clone <repo-url>
   cd CodebaseAtom
   ```

2. Buat konfigurasi lokal dengan menyalin `src/WebUI/appsettings.Example.json` menjadi `src/WebUI/appsettings.Development.json`.

   ```powershell
   Copy-Item src/WebUI/appsettings.Example.json src/WebUI/appsettings.Development.json
   ```

3. Sesuaikan konfigurasi pada `appsettings.Development.json`.

   - `Database:ConnectionString` adalah connection string SQL Server yang dipakai baik oleh database aplikasi maupun ASP.NET Core Identity.
   - `Database:DefaultPasswordForInitialUsers` adalah password akun awal yang dibuat saat seeding.
   - `FileStorage:FolderPath` adalah direktori lokal tempat file dokumen diunggah. Pastikan proses aplikasi memiliki izin tulis ke folder ini.

   Contoh untuk SQL Server Express LocalDB:

   ```json
   {
     "Database": {
       "ConnectionString": "Server=(LocalDB)\\MSSQLLocalDB;Database=CodebaseAtom;Trusted_Connection=True;",
       "DefaultPasswordForInitialUsers": "GantiDenganPasswordAman123!"
     },
     "FileStorage": {
       "FolderPath": "C:\\storages\\CodebaseAtom"
     }
   }
   ```

   Contoh menjalankan SQL Server 2022 dengan Docker:

   ```bash
   docker run --name codebaseatom-mssql -e ACCEPT_EULA=Y -e MSSQL_SA_PASSWORD=Your_password123 -p 1433:1433 -d mcr.microsoft.com/mssql/server:2022-latest
   ```

   Connection string yang sesuai untuk container tersebut:

   ```text
   Server=localhost,1433;Database=CodebaseAtom;User Id=sa;Password=Your_password123;TrustServerCertificate=True;
   ```

4. Jalankan aplikasi.

   ```bash
   dotnet run --project src/WebUI/WebUI.csproj
   ```

   Profil pengembangan bawaan menggunakan `https://localhost:44326`. Gunakan alamat yang ditampilkan di terminal bila berbeda.

## Database dan data awal

Ketika mulai berjalan, aplikasi menerapkan migrasi EF Core untuk database aplikasi dan database Identity, kemudian mengisi data awal. Kedua context menggunakan connection string yang sama, tetapi masing-masing memakai schema serta tabel riwayat migrasi sendiri.

Akun awal yang tersedia:

| Username | Role | Password |
| --- | --- | --- |
| `admin` | `Administrator` | Nilai `Database:DefaultPasswordForInitialUsers` |
| `condet` | — | Nilai `Database:DefaultPasswordForInitialUsers` |

Ubah password awal sebelum menggunakan aplikasi di lingkungan bersama. Seeding hanya membuat user yang belum ada; mengubah nilai konfigurasi tidak mengganti password user yang telah dibuat sebelumnya.

## Konfigurasi

`appsettings.json` berisi pengaturan umum aplikasi, sedangkan nilai yang spesifik untuk mesin pengembangan sebaiknya ditempatkan pada `appsettings.Development.json`. File konfigurasi pengembangan tersebut sudah diabaikan oleh Git.

| Section | Kegunaan |
| --- | --- |
| `Application` | Nama aplikasi dan perusahaan yang ditampilkan pada UI. |
| `Database` | Connection string SQL Server dan password akun awal. |
| `FileStorage` | Lokasi penyimpanan file dokumen yang diunggah. |
| `Logging` | Level log aplikasi dan framework. |
| `DetailedErrors` | Menampilkan detail error pada lingkungan pengembangan. |

## Rekomendasi dalam membaca source code

- `src/WebUI/Program.cs` — titik masuk aplikasi serta urutan middleware dan inisialisasi database.
- `src/WebUI/Components/Features/Projects` — halaman dan komponen pengelolaan project, work item, serta dokumen.
- `src/WebUI/Logics` — business logic yang dipisahkan berdasarkan use case.
- `src/WebUI/Infrastructure/Database` — `DatabaseContext`, konfigurasi entitas, migrasi, dan seeding data aplikasi.
- `src/WebUI/Infrastructure/Identity` — konfigurasi Identity, user/role awal, dan migrasi Identity.
- `src/WebUI/Infrastructure/FileStorage` — layanan penyimpanan dokumen di file system lokal.

## Catatan pengembangan

- Proyek memperlakukan warning sebagai error dan mengaktifkan analisis kode saat build.
- Migrasi dan seeding dijalankan otomatis saat startup; pastikan SQL Server dapat diakses sebelum menjalankan aplikasi.
- Folder file storage tidak disimpan ke Git. Jangan arahkan `FileStorage:FolderPath` ke direktori yang tidak dapat ditulis oleh aplikasi.

## Lisensi

Proyek ini dilisensikan di bawah [MIT License](LICENSE).

Copyright (c) 2026 Vioren Informatika Teknologi
