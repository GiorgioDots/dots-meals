import { Directive, ElementRef, Input, Renderer2 } from '@angular/core'

@Directive({
  selector: '[dotsButton]',
})
export class DotsButtonDirective {
  @Input() loading: boolean = false

  private spinner: HTMLSpanElement

  constructor(
    private el: ElementRef,
    private renderer: Renderer2,
  ) {
    this.spinner = this.renderer.createElement('span')
    this.renderer.addClass(this.spinner, 'spinner')
    this.renderer.setAttribute(this.spinner, 'role', 'status')
    this.renderer.setAttribute(this.spinner, 'aria-hidden', 'true')
    this.renderer.addClass(this.el.nativeElement, 'btn')
  }

  ngOnChanges(): void {
    if (this.loading) {
      this.renderer.appendChild(this.el.nativeElement, this.spinner)
      this.renderer.setProperty(this.el.nativeElement, 'disabled', true)
    } else {
      this.renderer.removeChild(this.el.nativeElement, this.spinner)
      this.renderer.setProperty(this.el.nativeElement, 'disabled', false)
    }
  }
}
