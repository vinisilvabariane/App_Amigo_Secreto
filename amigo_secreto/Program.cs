using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

class Program {
    static void Main() {
        Console.WriteLine("=== AMIGO SECRETO — GERAR LINKS ===\n");

        // Lista dos participantes
        List<string> nomes = new List<string>()
        {
            "Vinicius",
            "Enzo Gabriel",
            "Vitoria",
            "Teresinha",
            "Josie",
            "Jessica",
            "Rodrigo",
            "Enzo Ferdinando",
            "Rafael",
            "Vero",
            "Rosangela"
        };

        // Pasta PAGES na raiz do projeto
        string pastaPages = Path.Combine(Directory.GetCurrentDirectory(), "..", "..", "..", "pages");
        pastaPages = Path.GetFullPath(pastaPages); // normaliza

        Console.WriteLine($"Pasta de saída: {pastaPages}");

        // Criar pasta se não existir
        Directory.CreateDirectory(pastaPages);

        // 🔥 APAGAR TUDO dentro de pages/
        Console.WriteLine("\nLimpando pasta pages/ ...");

        foreach (string file in Directory.GetFiles(pastaPages))
            File.Delete(file);

        foreach (string dir in Directory.GetDirectories(pastaPages))
            Directory.Delete(dir, true);

        Console.WriteLine("Pasta pages/ limpa!\n");

        // Sorteio (derangement)
        List<string> sorteados = GerarDerangement(nomes);

        Console.WriteLine("Gerando links individuais...\n");

        // Criar arquivos HTML na pasta pages
        for (int i = 0; i < nomes.Count; i++) {
            string nome = nomes[i];
            string quemTirou = sorteados[i];

            // Nome do arquivo = Nome da pessoa.html
            string arquivo = Path.Combine(pastaPages, $"{nome}.html");

            string html = GerarHtml(nome, quemTirou);

            File.WriteAllText(arquivo, html, Encoding.UTF8);

            Console.WriteLine($"{nome}: {arquivo}");
        }

        Console.WriteLine("\nTudo pronto!");
    }

    // Gera permutação onde ninguém recebe ele mesmo
    static List<string> GerarDerangement(List<string> nomes) {
        Random rnd = new Random();
        List<string> sorteio;

        do {
            sorteio = nomes.OrderBy(x => rnd.Next()).ToList();
        }
        while (ExisteAutoSorteio(nomes, sorteio));

        return sorteio;
    }

    static bool ExisteAutoSorteio(List<string> originais, List<string> sorteados) {
        for (int i = 0; i < originais.Count; i++)
            if (originais[i] == sorteados[i])
                return true;
        return false;
    }

    // HTML da página - VERSÃO MELHORADA
    static string GerarHtml(string nome, string tirou) {
        return $@"
<!DOCTYPE html>
<html lang='pt-br'>
<head>
    <meta charset='UTF-8'>
    <meta name='viewport' content='width=device-width, initial-scale=1.0'>
    <title>Amigo Secreto</title>
    <style>
        :root {{
            --primary: #d32f2f;
            --primary-dark: #b71c1c;
            --secondary: #2196f3;
            --light: #f5f5f5;
            --dark: #333;
            --success: #4caf50;
        }}
        
        * {{
            margin: 0;
            padding: 0;
            box-sizing: border-box;
        }}
        
        body {{
            font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
            background: linear-gradient(135deg, #6a11cb 0%, #2575fc 100%);
            min-height: 100vh;
            display: flex;
            align-items: center;
            justify-content: center;
            padding: 20px;
            color: var(--light);
        }}
        
        .container {{
            width: 100%;
            max-width: 500px;
        }}
        
        .card {{
            background: rgba(255, 255, 255, 0.95);
            border-radius: 20px;
            padding: 40px 30px;
            box-shadow: 0 20px 40px rgba(0, 0, 0, 0.3);
            text-align: center;
            color: var(--dark);
            position: relative;
            overflow: hidden;
        }}
        
        .card::before {{
            content: '';
            position: absolute;
            top: 0;
            left: 0;
            right: 0;
            height: 5px;
            background: linear-gradient(90deg, #d32f2f, #2196f3, #4caf50);
        }}
        
        h2 {{
            color: var(--dark);
            margin-bottom: 15px;
            font-size: 1.8rem;
        }}
        
        .greeting {{
            font-size: 1.2rem;
            margin-bottom: 25px;
            color: #666;
        }}
        
        .result-label {{
            font-size: 1.1rem;
            margin-bottom: 15px;
            color: #555;
        }}
        
        .result-name {{
            font-size: 3rem;
            color: var(--primary);
            margin: 20px 0;
            font-weight: bold;
            text-shadow: 2px 2px 4px rgba(0,0,0,0.1);
            animation: pulse 2s infinite;
        }}
        
        .good-luck {{
            margin-top: 25px;
            font-size: 1.1rem;
            color: #666;
            font-style: italic;
        }}
        
        .confetti {{
            position: fixed;
            width: 15px;
            height: 15px;
            background-color: #f00;
            opacity: 0;
            z-index: 1000;
        }}
        
        .firework {{
            position: fixed;
            width: 8px;
            height: 8px;
            border-radius: 50%;
            box-shadow: 0 0 10px #fff;
            z-index: 1000;
        }}
        
        @keyframes pulse {{
            0% {{ transform: scale(1); }}
            50% {{ transform: scale(1.05); }}
            100% {{ transform: scale(1); }}
        }}
        
        @keyframes fadeIn {{
            from {{ opacity: 0; transform: translateY(20px); }}
            to {{ opacity: 1; transform: translateY(0); }}
        }}
        
        .card {{
            animation: fadeIn 1s ease-out;
        }}
        
        @media (max-width: 600px) {{
            .card {{
                padding: 30px 20px;
            }}
            
            .result-name {{
                font-size: 2.5rem;
            }}
            
            h2 {{
                font-size: 1.5rem;
            }}
        }}
    </style>
</head>
<body>
    <div class='container'>
        <div class='card'>
            <h2>🎁 Amigo Secreto 🎁</h2>
            <div class='greeting'>Olá, <strong>{nome}</strong>!</div>
            <div class='result-label'>O seu amigo secreto é:</div>
            <div class='result-name'>{tirou}</div>
            <div class='good-luck'>🎉 Boa sorte com o presente! 🎉</div>
        </div>
    </div>

    <script>
        // Sistema de fogos de artifício
        function createFirework() {{
            const firework = document.createElement('div');
            firework.className = 'firework';
            
            // Posição aleatória na tela
            const x = Math.random() * window.innerWidth;
            const y = Math.random() * window.innerHeight;
            
            // Cor aleatória
            const hue = Math.floor(Math.random() * 360);
            firework.style.backgroundColor = `hsl(${{hue}}, 100%, 50%)`;
            firework.style.boxShadow = `0 0 15px hsl(${{hue}}, 100%, 50%)`;
            
            // Posicionar
            firework.style.left = `${{x}}px`;
            firework.style.top = `${{y}}px`;
            
            document.body.appendChild(firework);
            
            // Animação de explosão
            setTimeout(() => {{
                firework.style.transform = 'scale(4)';
                firework.style.opacity = '0';
                
                // Remover após animação
                setTimeout(() => {{
                    if (firework.parentNode) {{
                        firework.parentNode.removeChild(firework);
                    }}
                }}, 600);
            }}, 10);
        }}
        
        // Sistema de confetes
        function createConfetti() {{
            const confetti = document.createElement('div');
            confetti.className = 'confetti';
            
            // Posição aleatória no topo
            const x = Math.random() * window.innerWidth;
            confetti.style.left = `${{x}}px`;
            confetti.style.top = '-20px';
            
            // Cor aleatória
            const colors = ['#d32f2f', '#2196f3', '#4caf50', '#ffeb3b', '#9c27b0'];
            const color = colors[Math.floor(Math.random() * colors.length)];
            confetti.style.backgroundColor = color;
            
            // Forma aleatória
            const isCircle = Math.random() > 0.5;
            if (isCircle) {{
                confetti.style.borderRadius = '50%';
            }}
            
            // Tamanho aleatório
            const size = 5 + Math.random() * 15;
            confetti.style.width = `${{size}}px`;
            confetti.style.height = `${{size}}px`;
            
            document.body.appendChild(confetti);
            
            // Animação de queda
            const duration = 3 + Math.random() * 2;
            const rotation = Math.random() * 720 - 360;
            
            confetti.style.transition = `all ${{duration}}s ease-in`;
            
            setTimeout(() => {{
                confetti.style.transform = `translateY(${{window.innerHeight + 50}}px) rotate(${{rotation}}deg)`;
                confetti.style.opacity = '0.7';
            }}, 10);
            
            // Remover após animação
            setTimeout(() => {{
                if (confetti.parentNode) {{
                    confetti.parentNode.removeChild(confetti);
                }}
            }}, duration * 1000);
        }}
        
        // Iniciar efeitos
        function startEffects() {{
            // Fogos a cada 400ms
            const fireworkInterval = setInterval(createFirework, 400);
            
            // Confetes a cada 100ms
            const confettiInterval = setInterval(createConfetti, 100);
            
            // Parar após 8 segundos
            setTimeout(() => {{
                clearInterval(fireworkInterval);
                clearInterval(confettiInterval);
            }}, 8000);
        }}
        
        // Iniciar efeitos quando a página carregar
        window.addEventListener('load', startEffects);
        
        // Também iniciar ao clicar na página
        document.body.addEventListener('click', startEffects);
    </script>
</body>
</html>";
    }
}