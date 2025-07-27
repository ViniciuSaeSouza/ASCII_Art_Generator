## Todo List:
Dia 1
- [x] Encontrar biblioteca de manipulação de imagens
`Dia 2`
- [x] Descobrir como carregar uma imagem
	- [x] Descobrir como iterar sobre os pixel's
	- [x] Descobrir que raios é `PixelFormat`
	- [ ] Pesquisar sobre a diferença entre os tipos de `PixelFormat` e quando usar diferentes tipos (mais leves, mais completos e etc)
`Dia 3`
- [x] Aprender mais sobre Gray Scale - [Wikipedia](https://en.wikipedia.org/wiki/Grayscale)
- [x] Como transformar a minha imagem em cinza
	- [x] O que é transformar em Cinza?
	- [x] Quais métodos/cálculos posso usar para fazer a conversão?
	- [ ] Depois de fazer os cálculos, que raios que eu faço com o resultado? (Dia 4)
`Dia 4`
- [x] Depois de fazer os cálculos, que raios que eu faço com o resultado...
	- [x] Como inserir um novo pixel no lugar do antigo com a escala de cinza
		- [x] Como construir um novo pixel?
		- [x] Quais e que tipos de valores eu preciso passar?

## Dia 1
Comecei entendo que raios é um pixel, depois fui pra oque é 'grayscale' e depois comecei a ver fórmulas para transformar rgb em um valor de luminosidade cinza e ai lembrei que não sei nem consumir uma imagem direito em C#.

Procurando um pouco na internet sem ajuda de IA eu encontrei um pacote chamado Magick que promete suportar mais de 100 tipos de arquivos e blá blá blá... só sei que parece bom, a documentação por enquanto tá fácil de ler, agora é ver como que é colocando a mão na massa.

...Testei o Magick e vi que tinha muita coisa diferente da documentação com os métodos e propriedades atuais que ele provém, como eu não quero ficar fuçando o código deles pra entender como funciona decidi mudar de pacote....

Dessa vez fui em uma que o GPT recomendou e que eu tinha visto nas pesquisas do Google: ImageSharp da SixLabors. vamos ver como ela se sai na mão de um incompetente.

Testei o primeiro método da documentação (.Resize()) e por enquanto tudo certo! to animado, finalmente consegui mexer com uma biblioteca de manipulação de imagem e parece que esse pacote vai oferecer MUITA ajuda, acho até um pouco injusto, vamos ver se eu ainda penso assim quando eu travar denovo.

 >Repositório do pacote Magick:
 >[GitHub](https://github.com/dlemstra/Magick.NET/blob/main/docs/Readme.md)
 >Repositório do pacote ImageSharp:
 >[GitHub](https://github.com/SixLabors/ImageSharp)
###### 19/07/2025 - 23:52
## Dia 2

CONSEGUI LER MEU PRIMEIRO PIXEL 
(PUTA QUE PARIU FINALMENTE):
![[Pasted image 20250721141848.png]]


Depois de dar uma leve sofrida tentando entender a documentação da API do ImageSharp eu finalmente comecei a entender algumas coisas.

Primeiro de tudo é como ler a ***imagem***, primeiro eu comecei usando o exemplo da seção *'Getting Started'* do tópico *'ImageSharp'*:
```c#
using(Image img = Image.Load(string path))
```

> (Definição da documentação da classe Image)

> [!NOTE] Class Image
> Encapsulates an image, which consists of the pixel data for a graphics image and its attributes. For the non-generic [Image](https://docs.sixlabors.com/api/ImageSharp/SixLabors.ImageSharp.Image.html) type, the pixel type is only known at runtime. [Image](https://docs.sixlabors.com/api/ImageSharp/SixLabors.ImageSharp.Image.html) is always implemented by a pixel-specific [Image<TPixel>](https://docs.sixlabors.com/api/ImageSharp/SixLabors.ImageSharp.Image-1.html) instance.

Esse método retorna uma classe `Image` genérica que contém métodos e propriedades gerais de manuseio de um objeto de imagem como:

| Type                                                                                                        | Name          | Description                 |
| ----------------------------------------------------------------------------------------------------------- | ------------- | --------------------------- |
| [Configuration](https://docs.sixlabors.com/api/ImageSharp/SixLabors.ImageSharp.Configuration.html)          | configuration | The global configuration.   |
| [PixelTypeInfo](https://docs.sixlabors.com/api/ImageSharp/SixLabors.ImageSharp.Formats.PixelTypeInfo.html)  | pixelType     | The pixel type information. |
| [ImageMetadata](https://docs.sixlabors.com/api/ImageSharp/SixLabors.ImageSharp.Metadata.ImageMetadata.html) | metadata      | The image metadata.         |
| [Size](https://docs.sixlabors.com/api/ImageSharp/SixLabors.ImageSharp.Size.html)                            | size          | The size in px units.       |
--- 
Porém, só com essa classe eu não estava conseguindo acessar nenhum pixel diretamente, apenas propriedades gerais dos pixels e da imagem... 
Me aprofundando um pouco mais, comecei a ler sobre a classe  `Image<TPixel>` e, mais especificamente, o tipo `<TPixel>` genérico que ela implementa.
Esse `<TPixel>` é uma classe genérica que representa uma estrutura `(struc)` de valores que representam a cor dado um determinado pixel chamado na API de `Pixel Format`.
Apresentada uma tabela de [Formatos de pixel (Pixel Formats)](https://docs.sixlabors.com/api/ImageSharp/SixLabors.ImageSharp.PixelFormats.html#structs)
procurei algumas estruturas que servissem o meu propósito (contenham valores de alfa e rgb) e utilizei uma que aparece bastante nos exemplos de código da documentação, o formato `[[Rgba32]]`

>[!NOTE] Rgba 32 doc's definition:
>Packed pixel type containing four 8-bit unsigned normalized values ranging from 0 to 255. The color components are stored in red, green, blue, and alpha order (least significant to most significant byte). 
>Ranges from [0, 0, 0, 0] to [1, 1, 1, 1] in vector form.

>[!NOTE] 
>JPG, PNG, etc., can store pixel data in different formats (e.g., RGB24, Grayscale, indexed color), but when you load with `[[Rgba32]]`, you're forcing all pixels to a consistent structure: 8-bit Red, Green, Blue, and Alpha.


Pesquisando um pouco mais (e pedindo pro gpt um pouco de direção de como me encontrar na documentação), pesquisei pelo mais óbvio que ainda não tinha feito, pesquisei por 'Pixel' (duh) e voilá, me apareceu a seção ***Working with pixel buffers***, essa seção me mostrou o exemplo de código:
``` C#
using Image<Rgba32> image = Image.Load<Rgba32>("my_file.png");
```
Agora consigo carregar minha imagem com uma classe que suporta iterações sobre seus pixels! 🎉🎉🎉

###### 21/07/2025 - 15:14
---
## DIA 3
Bom, agora que eu descobri como iterar sobre os pixels, preciso saber o que eu faço com eles...

Pesquisando mais sobre [[GrayScale]] na Wikipedia, li que uma imagem em cinza tem os 3 canais de cor RGB de cada [[Pixel]] convertidos para um valor que representa sua luminosidade.

Mas como eu encontro a luminosidade de um pixel a partir do seu RGB?
Graças a deus tem pessoas mais inteligentes que eu que já pensaram nisso e criaram uma fórmula matemática simples que soma os valores 
R, G e B com um peso associado a cada canal de cor considerando a sensibilidade do olho humano para percepção de luminosidade para cada tipo de cor (verde por exemplo é uma cor mais facilmente vista pelos nosso olhos pois ela geralmente é melhor em absorver e refletir luz).

Com isso, podemos usar a fórmula (onde os valores R,G,B da **fórmula** são os valores em **bytes** RGB do [[Pixel]]) para adquirir nosso valor de luminosidade: 
> $yLinear=0.299*R+0.587*G+0.114*B$
[^1]

Legal, e agora? o que eu faço com esse valor $yLinear$ da fórmula?

Lembrando do por que estávamos buscando o valor de luminosidade: *uma imagem em escala de cinza possui todos seus canais RGB convertidos para um valor de luminosidade*

Agora que temos nossa luminosidade, **basta trocar os valores** R, G e B do pixel em questão para a variável $yLinear$.

O problema chave aqui é a frase ***basta trocar os valores***, como que eu faço isso?

Infelizmente nessa o GPT acabou me dando um spoiler (e foi repreendido por isso (falei pra ele que não ia dar uma maçã para ele pois ele não está sendo um bom tutor)) mas o spoiler estava dado
![[Pasted image 20250727162214.png]]

com essa "dica" bem na cara do GPT eu já entendi que para substituir o pixel atual eu preciso criar um novo objeto da classe [[Rgba32]] e no seu construtor eu posso passar os valores de RGB e A.

Fiz um bom progresso hoje, volto no dia 4.

## DIA 4
CONSEGUI TRANSFORMAR A IMAGEM EM CINZA!!!!! ~~PUTA QUE PARIU VAMBORA~~🎉🎉
![[Pasted image 20250727154409.png]]
###### 27/07/2025 - 15:43

[^1]: *Existem outras fórmulas com algumas mudanças para diferentes tipos de aplicação de escala de cinza para diferentes mídias (vídeo/imagem) e qualidades de mídia* 
