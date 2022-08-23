function editName(){
    let appear = document.getElementById('editor')
    setTimeout(function(){appear.style.display = "block"}, 2000)
}

function removeName(){
    let disappear = document.getElementById('editor')
    setTimeout(function(){disappear.style.display = "none"}, 3000);
}

function BNBack(){
    setTimeout(function(){
        let disappear = document.getElementById('BNUpdate')
        let update = document.getElementById('BN')
        disappear.style.display = "none";
        update.style.display = "block";
    }, 6000)
}

function editSD(){
    let appear = document.getElementById('editSD')
    setTimeout(function(){appear.style.display = "block"}, 2000)
}

function removeSD(){
    let disappear = document.getElementById('editSD')
    setTimeout(function(){disappear.style.display = "none"}, 3000);
}

function SDBack(){
    setTimeout(function(){
        let disappear = document.getElementById('USD')
        let update = document.getElementById('SD')
        disappear.style.display = "none";
        update.style.display = "block";
    }, 6000)
}

function addS(){
    let appear = document.getElementById('addS')
    setTimeout(function(){appear.style.display = "block"}, 2000)
}

function removeS(){
    let disappear = document.getElementById('addS')
    setTimeout(function(){disappear.style.display = "none"}, 3000);
}

function editW(){
    let appear = document.getElementById('editW')
    setTimeout(function(){appear.style.display = "block"}, 2000)
}

function removeW(){
    let disappear = document.getElementById('editW')
    setTimeout(function(){disappear.style.display = "none"}, 3000);
}

function WBack(){
    setTimeout(function(){
        let disappear = document.getElementById('WUpdate')
        let update = document.getElementById('W')
        disappear.style.display = "none";
        update.style.display = "block";
    }, 6000)
}

function editI(){
    let appear = document.getElementById('editI')
    setTimeout(function(){appear.style.display = "block"}, 2000)
}

function removeI(){
    let disappear = document.getElementById('editI')
    setTimeout(function(){disappear.style.display = "none"}, 3000);
}

function IBack(){
    setTimeout(function(){
        let disappear = document.getElementById('UI')
        let update = document.getElementById('I')
        disappear.style.display = "none";
        update.style.display = "block";
    }, 6000)
}

function editN(){
    let appear = document.getElementById('editN')
    setTimeout(function(){appear.style.display = "block"}, 2000)
}

function removeN(){
    let disappear = document.getElementById('editN')
    setTimeout(function(){disappear.style.display = "none"}, 3000);
}

function notes(){
    let appear = document.getElementById("SNotes")
    appear.style.display = "block";
}

function close(){
    let disappear = document.getElementById('newStaff');
    disappear.style.display = "none";
}

function addA(){
    let appear = document.getElementById('addA')
    setTimeout(function(){appear.style.display = "block"}, 2000)
}

function removeA(){
    let disappear = document.getElementById('addA')
    setTimeout(function(){disappear.style.display = "none"}, 3000);
}

function address(){
    let appear = document.getElementById('newAddress')
    appear.style.display = "block"
}

function addAD(){
    let appear = document.getElementById('aOptions')
    setTimeout(function(){appear.style.display = "block"}, 2000)
}

function removeAD(){
    let disappear = document.getElementById('aOptions')
    setTimeout(function(){disappear.style.display = "none"}, 3000);
}

function newS(){
    let appear = document.getElementById('newStaff')
    appear.style.display = "block"
}

function switchName(dis,up){
    let disappear = document.getElementById(dis)
    let update = document.getElementById(up)
    disappear.style.display = "none";
    update.style.display = "block";
}