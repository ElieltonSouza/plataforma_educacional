using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using plataforma_educacional.Models;
using plataforma_educacional.Models.ViewModels;
using plataforma_educacional.Services;
using System.Threading.Tasks;

namespace plataforma_educacional.Controllers
{
    [AllowAnonymous]
    public class AccountController : Controller
    {
        private readonly UserManager<Usuario> _userManager;
        private readonly SignInManager<Usuario> _signInManager;
        private readonly Repositorio<Instituicao> _repositorioInstituicao;
        private readonly EmailService _emailService;

        public AccountController(UserManager<Usuario> userManager, SignInManager<Usuario> signInManager, EmailService emailService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _repositorioInstituicao = new Repositorio<Instituicao>();
            _emailService = emailService;
        }

        [HttpGet]
        public IActionResult RegisterAluno()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> RegisterAluno(RegisterAlunoViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new Usuario { UserName = model.Email, Email = model.Email, Nome = model.Nome };
                var result = await _userManager.CreateAsync(user, model.Senha);
                if (result.Succeeded)
                {
                    var code = GenerateValidationCode();
                    user.CodigoValidacao = code; // Salvar o código de validação no usuário
                    await _userManager.UpdateAsync(user); // Atualizar o usuário com o código de validação
                    await _emailService.SendValidationCodeAsync(user.Email, code);
                    return RedirectToAction("ValidateEmail", new { userId = user.Id });
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult RegisterProfessor()
        {
            ViewBag.Instituicoes = _repositorioInstituicao.Listar();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> RegisterProfessor(RegisterProfessorViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new Usuario { UserName = model.Email, Email = model.Email, Nome = model.Nome, InstituicaoId = model.InstituicaoId };
                var result = await _userManager.CreateAsync(user, model.Senha);
                if (result.Succeeded)
                {
                    var code = GenerateValidationCode();
                    user.CodigoValidacao = code; // Salvar o código de validação no usuário
                    await _userManager.UpdateAsync(user); // Atualizar o usuário com o código de validação
                    await _emailService.SendValidationCodeAsync(user.Email, code);
                    return RedirectToAction("ValidateEmail", new { userId = user.Id });
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            ViewBag.Instituicoes = _repositorioInstituicao.Listar();
            return View(model);
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(model.Email, model.Senha, model.LembrarMe, lockoutOnFailure: false);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError(string.Empty, "Tentativa de login inválida.");
            }
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult ValidateEmail(string userId)
        {
            ViewBag.UserId = userId;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ValidateEmail(string userId, ValidateEmailViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByIdAsync(userId);
                if (user != null && model.CodigoValidacao == user.CodigoValidacao)
                {
                    user.EmailConfirmed = true;
                    await _userManager.UpdateAsync(user);
                    return RedirectToAction("Login", "Account");
                }
                ModelState.AddModelError(string.Empty, "Código de validação inválido.");
            }
            ViewBag.UserId = userId;
            return View(model);
        }

        private string GenerateValidationCode()
        {
            var random = new Random();
            return random.Next(100000, 999999).ToString();
        }
    }
}