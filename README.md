# AuthJWT

Belajar autentikasi dan otorisasi di ASP.NET Core: register, login JWT, refresh token, dan RBAC.

## Fitur

- Register user dengan password ter-hash (`PasswordHasher`)
- Login, mengembalikan access token (JWT) dan refresh token
- Refresh token untuk menukar token lama dengan access token baru
- Role-based access control (RBAC): `User`, `Admin`, `SuperAdmin`
- Endpoint terproteksi dengan `[Authorize]` dan `[Authorize(Roles = ...)]`

## Stack

- .NET (ASP.NET Core Web API)
- Entity Framework Core
- PostgreSQL
- JWT Bearer (`HmacSha512`)
- Scalar untuk dokumentasi API

## Cara jalan

Prasyarat: .NET SDK, PostgreSQL, dan `dotnet-ef`.

```bash
dotnet tool install --global dotnet-ef
```

1. Clone repo, masuk ke folder project:

```bash
git clone <url-repo>
cd AuthJWT
```

2. Set secret lewat user-secrets. Token wajib minimal 64 byte karena memakai `HmacSha512`:

```bash
dotnet user-secrets set "AppSettings:Token" "$(openssl rand -base64 64 | tr -d '\n')"
dotnet user-secrets set "ConnectionStrings:UserDatabase" "Host=localhost;Port=5432;Database=authjwt;Username=postgres;Password=<password-kamu>"
```

3. Buat database dan tabel:

```bash
dotnet ef database update
```

4. Jalankan:

```bash
dotnet run
```

5. Buka dokumentasi API di `http://localhost:<port>/scalar/v1`. Port tertera di output `dotnet run`.

## Endpoint

| Method | Path                      | Akses                 | Keterangan                                          |
| ------ | ------------------------- | --------------------- | --------------------------------------------------- |
| POST   | `/api/Auth/register`      | Publik                | Daftar user baru, role default `User`               |
| POST   | `/api/Auth/login`         | Publik                | Login, mengembalikan access token dan refresh token |
| POST   | `/api/Auth/refresh-token` | Publik                | Tukar refresh token dengan token baru               |
| GET    | `/api/Auth`               | Login                 | Hanya untuk user yang sudah login                   |
| GET    | `/api/Auth/admin-only`    | `Admin`, `SuperAdmin` | Hanya untuk admin                                   |

Kirim access token lewat header:

```
Authorization: Bearer <access-token>
```

## Role

User baru otomatis berperan `User`. Role tidak bisa dipilih saat register, supaya orang tak bisa mengangkat dirinya jadi admin. Untuk mencoba endpoint admin, ubah kolom `Role` di tabel `Users` jadi `Admin` lewat pgAdmin, lalu login ulang. Token lama masih membawa role lama.

## Konfigurasi

`appsettings.json` berisi placeholder. Nilai asli disimpan di user-secrets dan tidak masuk repo. Nilai user-secrets menimpa `appsettings.json`.

## Catatan

- Waktu disimpan dalam UTC.
- Access token berlaku 15 menit, refresh token juga 15 menit.
- Project ini untuk belajar. Refresh token masih disimpan plain text di database, sedangkan untuk production sebaiknya di-hash.
