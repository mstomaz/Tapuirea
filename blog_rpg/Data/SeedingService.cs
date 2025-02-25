using blog_rpg.Models;

namespace blog_rpg.Data
{
    public class SeedingService
    {
        private readonly BlogContext _context;

        public SeedingService(BlogContext context)
        {
            _context = context;
        }

        public void Seed()
        {
            if (_context is null)
                return;

            if (!_context.Users.Any() || !_context.Tales.Any())
            {
                User u1 = new User("1", "Mateus", Models.Enums.UserRole.Author, Models.Enums.Country.Brazil, new DateTime(2000, 1, 2));
                _context.Users.Add(u1);
                _context.SaveChanges();

                Tale t1 = new Tale(1, u1, "O Cultivador das Chamas", "No Bosque Divino – hoje conhecido como o Bosque Carbonizado – erguia-se o Tronco de Ouro, uma das árvores mais antigas de [Insira o nome do mundo aqui]. Seu nome vinha do brilho dourado de seu tronco, uma manifestação de sua energia vital única. Essa árvore era reverenciada pelos habitantes do bosque, compostos principalmente pelos taroas, humanoides com ligação espiritual à natureza, e pelos florais, seres cuja forma misturava características vegetais e animais, além de alguns patrulheiros yokais e humanos.\r\n\r\nDizia-se que o Tronco de Ouro possuía um poder natural incomparável, transformando o solo ao seu redor em um terreno fértil, onde tudo florescia. Graças a ele, a sociedade no Bosque Divino era próspera e autossuficiente. Os taroas e florais viviam em harmonia, desfrutando de uma existência pacífica e igualitária. Apesar de haver uma hierarquia natural em sua organização, todos tinham acesso aos mesmos recursos abundantes fornecidos pela terra.\r\n\r\nConflitos surgiam apenas com invasores externos – gananciosos que cobiçavam os animais raros do bosque ou a madeira valiosa de suas árvores. Contudo, tais ameaças eram prontamente eliminadas, pois os taroas, com sua ligação mágica ao ambiente, e os florais, com suas formas adaptadas para a batalha, protegiam a floresta com eficiência brutal.\r\n\r\nNo entanto, nenhuma utopia é eterna. O mundo, movido por sangue e ganância, destrói até os mais belos sonhos.\r\n\r\n\r\nCerto dia, um homem humano – ganancioso e vil – invadiu o Bosque Divino. Fugindo de criminosos após tentar enganá-los em um acordo, ele cavalgava desesperado até que uma flecha atingiu seu cavalo. Homem e montaria caíram no chão; o impacto quebrou sua perna esquerda, deixando-o indefeso em uma clareira.\r\n\r\nOs perseguidores o cercaram, rindo cruelmente.\r\n– Você traiu a confiança errada, verme – disse um homem em armadura completa. – E tentou fugir como o covarde que é! Agora pagará com sangue!\r\n\r\nAntes que o algoz pudesse desferir o golpe final, uma flecha certeira atravessou o vão entre seu elmo e a armadura, encerrando sua vida.\r\n– SELVAGEN... – começou a gritar uma mulher, antes de ser atacada por um floral. Seu braço esquerdo, semelhante ao tronco de uma árvore, envolveu-a como uma videira, enquanto seu braço direito, que tinha garras como as de uma onça, rasgaram o peito da mulher.\r\n\r\nO último perseguidor tentou fugir, mas foi perfurado por outro floral, cujo braço terminava em uma estaca de madeira. A clareira ficou silenciosa, exceto pelos gritos do homem ganancioso. Os florais o levaram para a vila, onde seria interrogado.\r\n\r\nChegando à vila, o homem ficou maravilhado com a majestade do Tronco de Ouro. Para ele, aquilo era mais do que uma árvore; era um bilhete dourado para sua fortuna. Durante o interrogatório, mentiu, afirmando ter roubado dos criminosos para alimentar sua família. Compadecidos, os taroas curaram sua perna, ofereceram abrigo e, como prova de boa fé, deram-lhe uma lasca do Tronco de Ouro.\r\n\r\nAlguns dias depois, o homem partiu, com um sorriso malicioso e planos ainda mais maléficos.\r\n\r\n\r\nDe volta à cidade, o homem apresentou a lasca ao chefe dos bandidos, o mesmo que havia enviado os perseguidores atrás dele. Apesar do risco de enfrentar o homem, apostou alto. Sabia que apenas aquele bandido, com sua ganância e recursos, poderia reunir um exército de mercenários para invadir o bosque.\r\n\r\nConvencido, o chefe dos bandidos perdoou sua dívida e mobilizou seus homens. Armados com lâminas e tochas, eles marcharam para o Bosque Divino semanas depois.\r\n\r\nA devastação foi implacável. Animais raros foram caçados, a flora foi incendiada, e o caos reinou. No coração do bosque, os taroas e florais resistiram com bravura ao redor do Tronco de Ouro, mas foram subjugados. Apesar das pesadas baixas entre os invasores, o bosque sucumbiu.\r\n\r\nO homem ganancioso encontrou seu fim pelas mãos do próprio líder dos bandidos, traído em nome da ganância. Contudo, o estrago já estava feito: o Tronco de Ouro havia sido profanado, e os taroas e florais, exterminados.\r\n\r\n\r\nUm único sobrevivente, um floral sem nome, contemplou o massacre com ódio e desespero. Ferido e em chamas, ele realizou um último ato: bebeu a seiva do Tronco de Ouro, misturada com sangue e cinzas. A mágica vital da árvore e o fogo que a consumia o transformaram.\r\n\r\nO floral renasceu como uma entidade terrível: O Cultivador das Chamas, uma fusão de madeira e fogo, ódio e destruição. Ele dizimou o exército invasor, reduzindo o bosque a cinzas. Sua fúria era imparável, e o antigo Bosque Divino tornou-se o Bosque Carbonizado, um local desolado, onde as chamas nunca se apagam.\r\n\r\nMas o Cultivador das Chamas estava preso em um dilema. Resquícios de sua antiga consciência ainda lutavam para proteger o bosque que ele tanto amava, enquanto seu novo ser, consumido pelo fogo, buscava vingança cega contra o responsável pela destruição.\r\n\r\nCego pelo ódio, o Cultivador falhava em perceber que o homem ganancioso já estava morto desde o início de sua transformação. Agora, ele reside no Bosque Carbonizado, perpetuando o legado das chamas, preso em uma existência de vingança e solidão.", DateTime.Now, null);
                _context.Tales.Add(t1);
                _context.SaveChanges();
            }
        }
    }
}
