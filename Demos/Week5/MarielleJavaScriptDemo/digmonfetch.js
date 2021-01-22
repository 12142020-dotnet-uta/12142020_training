function GetDigimon()
{
    let digiName = document.querySelector('#digimonInput').value;
    fetch(`https://digimon-api.vercel.app/api/digimon/name/${digiName}`)
    .then(result => result.json())
    .then(data => processResult(data));
}
function processResult(data)
{
    document.querySelector('.digimonResult img').setAttribute('src', data[0].img);
    document.querySelectorAll('.digimonResult caption').forEach((element) => element.remove());
    let caption = document.createElement('caption');
    caption.appendChild(document.createTextNode(data[0].name));
    document.querySelector('.digimonResult').appendChild(caption);
    document.querySelector('#digimonInput').value = '';
}