# Домашнее задание - Products API (JWT)

Запуск: `dotnet run` -> http://localhost:5063

| Требование | Реализация / файл |
|---|---|
| Модель Product (Id, Name, Price, Category) | `Models/Product.cs` |
| GET /api/products, GET /api/products/{id} - без авторизации | `Controllers/ProductsController.cs`, `02_get_list.png`, `03_get_one.png` |
| POST /api/products, DELETE /api/products/{id} - только с JWT (`[Authorize]`) | `04_post_no_token.png` (401), `07_delete_no_token.png` (401) |
| `POST /api/auth/login`, JWT с Issuer, Audience, сроком действия, ключом подписи | `Controllers/AuthController.cs`, `Program.cs`, `appsettings.json` |
| Скриншот: получение JWT | `01_login_token.png` |
| Скриншот: защищённый API без токена | `04_post_no_token.png` -> 401 |
| Скриншот: Authorization: Bearer <token> | `05_post_with_bearer.png` |
| Скриншот: успешное добавление товара | `05_post_with_bearer.png` -> 201 Created, `06_get_new.png` |
| Скриншот: неправильный / просроченный токен | `09_tampered.png`, `10_expired.png`, `11_garbage.png` -> 401 |
| Доп. задание: Claim с именем пользователя | claim `name` в JWT; `POST /api/products` читает его через `HttpContext.User.Claims` и возвращает `createdBy` |

Итоговая таблица: `99_summary.png`.
