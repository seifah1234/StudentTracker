# StudentTracker

نظام لمتابعة مستوى الطلاب، بيساعد المدرس يسجّل الدرجات والحضور، ويطلعله تصنيف تلقائي لمستوى كل طالب مع تحليل بالـ AI.

---

## المحتويات
- [نظرة عامة](#نظرة-عامة)
- [المعمارية](#المعمارية)
- [المتطلبات](#المتطلبات)
- [تشغيل الـ Backend (.NET)](#تشغيل-الـ-backend-net)
- [تشغيل خدمة الـ AI (Python)](#تشغيل-خدمة-الـ-ai-python)
- [متغيرات البيئة](#متغيرات-البيئة)
- [هيكل المشروع](#هيكل-المشروع)
- [الفريق](#الفريق)

---

## نظرة عامة

المشروع بيتكون من جزئين شغالين مع بعض:

| الجزء | التقنية | المسؤولية |
|---|---|---|
| **Backend API** | ASP.NET Core (.NET 8) | Auth, CRUD, قاعدة البيانات، اللوجيك العام |
| **AI Service** | Python (FastAPI) | تحليل درجات وحضور الطالب وإطلاع تصنيف/توصيات |

الـ Backend مبني بطبقات **PL / BLL / DAL**، وبيكلم خدمة الـ AI عن طريق HTTP لما يحتاج تحليل.

---

## المعمارية

```
┌────────────────────────────┐        HTTP/REST        ┌───────────────────────────┐
│   StudentTracker.Api (.NET) │ ───────────────────────►│  StudentTracker.AI (Python)│
│   PL → BLL → DAL             │                          │   FastAPI Service          │
│   Auth / CRUD / DB           │ ◄─────────────────────── │   تحليل بيانات + تصنيف    │
└────────────────────────────┘        JSON Response      └───────────────────────────┘
              │
              ▼
        SQL Server / SQLite
```

---

## المتطلبات

- [.NET 8 SDK](https://dotnet.microsoft.com/download)
- [Python 3.11+](https://www.python.org/downloads/)
- SQL Server (أو SQLite للتجربة المحلية)
- Visual Studio 2022 / VS Code

---

## تشغيل الـ Backend (.NET)

```bash
cd StudentTracker.PL

# استرجاع الحزم
dotnet restore

# تطبيق الـ Migrations وإنشاء قاعدة البيانات
dotnet ef database update

# تشغيل المشروع
dotnet run
```

بعد التشغيل، Swagger هيبقى متاح على:
```
https://localhost:{port}/swagger
```

---

## تشغيل خدمة الـ AI (Python)

```bash
cd StudentTracker.AI

# إنشاء بيئة افتراضية
python -m venv venv
source venv/bin/activate      # على Windows: venv\Scripts\activate

# تثبيت الحزم
pip install -r requirements.txt

# تشغيل الخدمة
uvicorn main:app --reload --port 8000
```

هيبقى متاح على:
```
http://localhost:8000/docs   (Swagger UI الخاص بـ FastAPI)
```

---

## متغيرات البيئة

اعملوا نسخة من كل ملف `.example` وسموها بدون `.example`، واملأوا القيم الحقيقية (متترفعش على Git):

**`StudentTracker.PL/appsettings.Development.json`**
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=.;Database=StudentTrackerDb;Trusted_Connection=True;TrustServerCertificate=True;"
  },
  "Jwt": {
    "Key": "your-secret-key",
    "Issuer": "StudentTracker",
    "ExpiryMinutes": 60
  },
  "AiService": {
    "BaseUrl": "http://localhost:8000"
  }
}
```

**`StudentTracker.AI/.env`**
```
ANTHROPIC_API_KEY=your-key-here
```

---

## هيكل المشروع

```
StudentTracker/
├── StudentTracker.PL/        # Web API (Controllers, Program.cs)
├── StudentTracker.BLL/       # Business Logic (Services, DTOs)
├── StudentTracker.DAL/       # Data Access (Entities, Repositories, DbContext)
├── StudentTracker.Shared/    # Enums / Constants مشتركة
├── StudentTracker.AI/        # خدمة تحليل بالـ AI (Python/FastAPI)
├── .gitignore
└── README.md
```

التفاصيل الكاملة لكل مجلد وملف موجودة في [StudentTracker-ProjectStructure.md](./StudentTracker-ProjectStructure.md).

---

## الفريق

- عضو 1 — Backend (.NET)
- عضو 2 — AI Service (Python)

---

## حالة المشروع

🚧 قيد التطوير
