# Практическая работа - ProductApi (API Key + JWT)

Запуск: `dotnet run` -> http://localhost:5161/swagger

| Задание | Реализация | Скриншот |
|---|---|---|
| 1. Web API, модель `Product`, `GET /api/product` | `Controllers/ProductController.cs`, `Models/Product.cs`, Swagger | `00_swagger_list.png` |
| 2. API Key в заголовке `X-Api-Key: SecretKey123` | `Filters/ApiKeyAttribute.cs` (ключ в `appsettings.json`) | `01_apikey_missing.png` (401), `02_apikey_wrong.png` (401), `03_apikey_ok.png` (200) |
| 3. Настройка JWT (Issuer, Audience, IssuerSigningKey, срок, ClockSkew) | `Program.cs` | - |
| 4. `POST /api/auth/login` -> JWT | `Controllers/AuthController.cs` | `05_login_token.png` (+ `04_login_wrong.png`) |
| 5. `[Authorize]` на методе | `GET /api/product/secure` | `06_secure_no_token.png` (401), `00b_swagger_secure_no_token.png` |
| 6. `Authorization: Bearer <token>` | - | `07_secure_with_token.png` (200) |
| 7. Четыре ситуации | - | `07_secure_with_token.png` (200), `06_secure_no_token.png` (401), `08_tampered.png` (401), `09_expired.png` (401), сводка `99_summary.png` |

HTTP-коды задания 7: без токена **401**, корректный JWT **200**, изменённый JWT **401**, истёкший JWT **401**.

Примечание: `GET /api/product` защищён API-ключом (задание 2), `GET /api/product/secure` - JWT (задание 5). Это два независимых механизма; API-ключ не заменяет JWT (`10_extra_apikey_on_jwt.png`).
