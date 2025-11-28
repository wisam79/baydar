# Beidar POS

نظام نقطة بيع (Point of Sale - POS) احترافي مبني باستخدام C# و .NET 8/9 WPF.

## التقنيات المستخدمة
- **WPF** (Windows Presentation Foundation)
- **.NET 9.0**
- **Entity Framework Core** (SQLite)
- **ModernWPF UI** & **Material Design In XAML**
- **MVVM Pattern** (CommunityToolkit.Mvvm)
- **Dependency Injection**

## المتطلبات
- .NET 9.0 SDK
- Visual Studio 2022 أو VS Code

## كيفية التشغيل
1. تأكد من تثبيت .NET 9.0 SDK.
2. انتقل إلى مجلد المشروع.
3. قم ببناء المشروع:
   ```bash
   dotnet build
   ```
4. قم بتشغيل التطبيق:
   ```bash
   dotnet run --project Beidar.UI
   ```

## هيكلة المشروع
- **Beidar.Core**: يحتوي على النماذج (Models) والواجهات (Interfaces).
- **Beidar.Data**: يحتوي على سياق قاعدة البيانات (DbContext) والمستودعات (Repositories).
- **Beidar.UI**: واجهة المستخدم (WPF App).

## حالة التطوير الحالية
- تم إنشاء البنية الأساسية للمشروع.
- تم إعداد قاعدة البيانات (SQLite) وتطبيق Migrations.
- تم تنفيذ نمط Repository & Unit of Work.
- تم إعداد واجهة المستخدم الأساسية وتكوين Dependency Injection.