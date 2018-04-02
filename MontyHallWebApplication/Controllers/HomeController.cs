using MontyHallLibrary;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace MontyHallWebApplication.Controllers
{
    public class HomeController : Controller
    {
        /// <summary>
        /// Счетчик количества побед
        /// </summary>
        //private int wins = 0;
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Получить процент побед при выбранной стратегии и выбранном количестве игр
        /// </summary>
        /// <param name="strategy"> true - менять выбор двери, false - не менять </param>
        /// <param name="count"> Количество игр </param>
        /// <returns></returns>
        [HttpPost]
        public async Task<JsonResult> StartPlaying(bool strategy, int count)
        {
            int wins = await PlayManyTimesAsync(new Player(strategy), count);
            double result = ((double)wins / count) * 100;

            wins = 0;

            return Json(new { count, strategy, result });
        }

        /// <summary>
        /// Играет count раз и считает количество побед
        /// </summary>
        private Task<int> PlayManyTimesAsync(Player player, int count)
        {
            return Task.Run(() => {
                int wins = 0;
                for (int i = 0; i < count; i++) {
                    if (player.Play())
                        wins++;
                }

                return wins;
            });
        }
    }
}