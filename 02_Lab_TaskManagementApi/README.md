# Лабораторная работа - Task Management API (JWT)

Запуск: `dotnet run` -> http://localhost:5062

## Что необходимо предоставить -> где лежит
| Требование | Файл |
|---|---|
| Исходный код проекта | вся папка |
| Program.cs с настройкой JWT | `Program.cs` |
| AuthController | `Controllers/AuthController.cs` |
| TasksController | `Controllers/TasksController.cs` |
| Скриншот получения JWT | `screenshots/01_login_token.png` |
| Скриншот запроса без токена - 401 | `screenshots/05_post_no_token.png` |
| Скриншот успешного запроса с JWT | `screenshots/06_post_with_jwt.png`, `08_delete_with_jwt.png` |
| Пример содержимого Claims | `screenshots/11_claims.png` (ответ `GET /api/auth/me`), декодированный payload - на `01_login_token.png` |
| Таблица проверок безопасности (раздел 9) | `screenshots/99_summary.png` |

## Проверка безопасности (раздел 9)
| № | Проверка | Ожидается | Получено |
|---|---|---|---|
| 1 | GET без токена | 200 OK | 200 OK |
| 2 | POST без токена | 401 Unauthorized | 401 Unauthorized |
| 3 | POST с корректным JWT | Успех | 201 Created (`createdBy: "student"`) |
| 4 | DELETE с корректным JWT | Успех | 200 OK (`deletedBy: "student"`) |
| 5 | Запрос с изменённым JWT | 401 | 401 (подпись не прошла проверку) |
| 6 | Запрос с истёкшим JWT | 401 | 401 (`The token expired at ...`) |

## Claims
В токене: `name = student`, `role = User` (+ `jti`, `exp`, `iss`, `aud`). Имя пользователя читается в защищённых методах через
`HttpContext.User.Claims.FirstOrDefault(c => c.Type == "name")` и возвращается вместе с результатом операции (`createdBy` / `deletedBy`).
В `Program.cs` отключено переименование claim'ов (`MapInboundClaims = false`), поэтому типы остаются `name` и `role`.

## Краткий вывод
В работе реализован Task Management API на ASP.NET Core с JWT-аутентификацией. Токен выдаётся методом `POST /api/auth/login` после проверки
логина и пароля, содержит Issuer, Audience, claims `name` и `role`, ограниченный срок действия и подпись HS256. Сервер проверяет подпись,
Issuer, Audience и срок жизни токена, а секретный ключ хранится только на сервере. Методы чтения задач открыты, а создание и удаление
доступны только с атрибутом `[Authorize]` и корректным токеном. Тесты подтвердили ожидаемое поведение: запросы без токена,
с изменённым и с истёкшим токеном получают 401 Unauthorized, а запросы с корректным токеном выполняются успешно.
