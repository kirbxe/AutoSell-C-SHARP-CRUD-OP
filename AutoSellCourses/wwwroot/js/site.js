let slideIndex = 0;
const slides = document.getElementsByClassName('carousel-item');
function showSlide(n){

    if(n >= slides.length){
        slideIndex = 0;
    }

    if(n < 0){
        slideIndex = slides.length - 1;
    }

    for(let i = 0; i < slides.length; i++){
        slides[i].classList.remove('active')
    }

    slides[slideIndex].classList.add('active')

    }

function incrementSlide(){
showSlide(slideIndex += 1)
}
function decrementSlide(){
showSlide(slideIndex -= 1)
}

showSlide(slideIndex)

const popUpClose = document.getElementById('pop-up-close');

const popUpContainer = document.getElementById('pop-up-conteiner')


popUpClose.addEventListener('click', function(e) {
popUpContainer.classList.remove('active')    
})




function showFormPopUp(){
    popUpContainer.classList.add('active')
}


popUpContainer.addEventListener('click', event => {

    if (event.target === popUpContainer) {

        popUpContainer.classList.remove('active')
    }
})











