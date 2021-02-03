
//let xhr = new XMLHttpRequest();

//setup here below.....
// xhr.onreadystatechange = chuckNorris;

// function chuckNorris(child) {
//     //if (this.readyState == 4 && this.status == 200) {
//         //let pArray = document.getElementsByTagName('p');
//         pArray[child].innerHTML = xhr.responseText;            
//     //}
// }



// xhr.open('GET', 'http://api.icndb.com/jokes/random', true);
// xhr.send();

// NOW to put a diferent joke into each <p>
//let pArray = document.getElementsByTagName('p');
 
//Array.prototype.forEach.call(pArray, child => {

// for (let i = 0; i < pArray.length; ){  
    
//     setTimeout(function () {
//         console.log(i);
//         xhr.onreadystatechange = function () {
//             if (xhr.readyState == 4 && xhr.status == 200) {
//                 pArray[i].innerHTML = xhr.responseText;
//             }
//         }
//         if (xhr.readyState == 4 && xhr.status == 200) {
//             xhr.open('GET', 'http://api.icndb.com/jokes/random', true);
//             xhr.send();
//         }
//     },5000);
// };


// BELOW IS Fetch() GET request demo
// let ps = document.getElementsByTagName('p');

// for (let i = 0; i < ps.length; i++){
// fetch('http://api.icndb.com/jokes/random')
//     .then(response => response.json(), err => alert('There was an error'))
//     .then(response1 => {
//         ps[i].innerHTML = response1.value.joke
//     })
//     .finally(alert('Hey there Chuck. Please spare my life, sir'))
// }


// below is a fetch() POST request
// fetch('https://jsonplaceholder.typicode.com/todos/1')
//   .then(response => response.json())
//   .then(json => console.log(json.title + ' fetch me some more'))

let obj = {
    method: 'POST',
    body: JSON.stringify({
        title: 'Master',
        body: 'Chess Player',
        userId: 3,
    }),
    headers: {
        'Content-type': 'application/json; charset=UTF-8'
    },
};

fetch('https://jsonplaceholder.typicode.com/posts', obj)
    .then(res => res.json(), err => alert('There was an error'))
    .then(res1 => {
        let ps = document.getElementsByTagName('p');
        ps[1].innerHTML = `Dude #${res1.id} goes by the name of ${res1.title} ${res1.body}. He's #${res1.userId} in my book.`
    });


