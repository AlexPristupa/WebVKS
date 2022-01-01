import '@babel/polyfill'

export default {
  bind(el, binding, vnode) {
    const dialogHeaderEl = el.querySelector('.el-dialog__header')
    const dragDom = el.querySelector('.el-dialog')
    dialogHeaderEl.style.cssText += ';cursor:move;'
    dragDom.style.cssText += ';top:0px;'

    // Возвращает исходные свойства элемента IE dom.currentStyle Firefox Google window.getComputedStyle (элемент dom, null);
    const getStyle = (function() {
      if (window.document.currentStyle) {
        return (dom, attr) => dom.currentStyle[attr]
      } else {
        return (dom, attr) => getComputedStyle(dom, false)[attr]
      }
    })()

    const hideDropdowns = function() {
      const dropdowns = Array.from(
        document.getElementsByClassName('el-select-dropdown'),
      )
      if (dropdowns.length) {
        dropdowns.forEach(element => {
          if (element.style.display !== 'none') {
            element.style.display = 'none'
          }
        })
      }
    }

    dialogHeaderEl.onmousedown = e => {
      if (!e.target.className.includes('el-icon')) {
        hideDropdowns()

        // Нажать кнопку мыши, чтобы вычислить расстояние текущего элемента от видимого экрана
        const disX = e.clientX - dialogHeaderEl.offsetLeft
        const disY = e.clientY - dialogHeaderEl.offsetTop

        const dragDomWidth = dragDom.offsetWidth
        const dragDomHeight = dragDom.offsetHeight

        const screenWidth = document.body.clientWidth
        const screenHeight = document.body.clientHeight

        const minDragDomLeft = dragDom.offsetLeft
        const maxDragDomLeft = screenWidth - dragDom.offsetLeft - dragDomWidth

        const minDragDomTop = dragDom.offsetTop
        const maxDragDomTop = screenHeight - dragDom.offsetTop - dragDomHeight

        // Значение, полученное с px-регулярным совпадением подстановки
        let styL = getStyle(dragDom, 'left')
        let styT = getStyle(dragDom, 'top')

        if (styL.includes('%')) {
          styL = +document.body.clientWidth * (+styL.replace(/%/g, '') / 100)
          styT = +document.body.clientHeight * (+styT.replace(/%/g, '') / 100)
        } else {
          styL = +styL.replace(/\px/g, '')
          styT = +styT.replace(/\px/g, '')
        }

        document.onmousemove = function(e) {
          // С помощью делегата события вычисляется пройденное расстояние
          let left = e.clientX - disX
          let top = e.clientY - disY

          // Обработка границ
          if (-left > minDragDomLeft) {
            left = -minDragDomLeft
          } else if (left > maxDragDomLeft) {
            left = maxDragDomLeft
          }

          if (-top > minDragDomTop) {
            top = -minDragDomTop
          } else if (top > maxDragDomTop) {
            top = maxDragDomTop
          }

          // Перемещение текущего элемента
          dragDom.style.cssText += `;left:${left + styL}px;top:${top + styT}px;`

          // вызвать onDrag событие
          vnode.child.$emit('dragDialog')
        }

        document.onmouseup = function() {
          document.onmousemove = null
          document.onmouseup = null
        }
      }
    }
  },
}
