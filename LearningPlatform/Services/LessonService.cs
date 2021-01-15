using LearningPlatform.Data;
using LearningPlatform.ViewModels;

namespace LearningPlatform.Services
{
    public static class LessonService
    {
        public static void ResolveOrders(ApplicationDbContext db, StudyCourseViewModel model, int oldOrder)
        {
            var order = model.Lesson.Order;
            if (order == oldOrder) return;
            if (oldOrder == -1) oldOrder = int.MaxValue;
            
            foreach (var l in model.Module.Lessons)
            {
                if (order < oldOrder || oldOrder == int.MaxValue)
                {
                    if (l.Order >= order && l.Order < oldOrder && l.Id != model.Lesson.Id) l.Order++;
                    else continue;
                }
                else
                {
                    if (l.Order > oldOrder && l.Order <= order && l.Id != model.Lesson.Id) l.Order--;
                    else continue;
                }

                db.Lessons.Update(l);
            }

            foreach (var a in model.Module.Assignments)
            {

                if (order < oldOrder || oldOrder == int.MaxValue)
                {
                    if (a.Order >= order && a.Order < oldOrder) a.Order++;
                    else continue;
                }
                else
                {
                    if (a.Order > oldOrder && a.Order <= order) a.Order--;
                    else continue;
                }

                db.Assignments.Update(a);
            }

            db.SaveChanges();
        }

    }
}