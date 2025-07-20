[Authorize]
public class MessageController : Controller
{
    private readonly ApplicationDbContext _context;
    private readonly UserManager<ApplicationUser> _userManager;

    public MessageController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
    {
        _context = context;
        _userManager = userManager;
    }

    public async Task<IActionResult> Index()
    {
        var userId = _userManager.GetUserId(User);
        var threads = await _context.MessageThreads
            .Where(t => t.PatientId == userId || t.DoctorId == userId)
            .Include(t => t.Messages)
            .ToListAsync();

        return View(threads);
    }

    public async Task<IActionResult> ViewThread(int id)
    {
        var thread = await _context.MessageThreads
            .Include(t => t.Messages)
            .FirstOrDefaultAsync(t => t.Id == id);

        return View(thread);
    }

    [HttpPost]
    public async Task<IActionResult> SendMessage(int threadId, string content)
    {
        var userId = _userManager.GetUserId(User);

        var message = new Message
        {
            ThreadId = threadId,
            SenderId = userId,
            Content = content,
            SentAt = DateTime.Now,
            IsRead = false
        };

        _context.Messages.Add(message);
        await _context.SaveChangesAsync();

        return RedirectToAction("ViewThread", new { id = threadId });
    }
}
