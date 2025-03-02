import { Component, Input, Output, EventEmitter } from '@angular/core'
import { CommonModule } from '@angular/common'

export type ButtonVariant = 'default' | 'destructive' | 'outline' | 'secondary' | 'ghost' | 'link'
export type ButtonSize = 'default' | 'sm' | 'lg' | 'icon'
export type ButtonTypes = 'button' | 'submit'

@Component({
  selector: 'app-button',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './button.component.html',
})
export class ButtonComponent {
  @Input() type: ButtonTypes = 'button'
  @Input() variant: ButtonVariant = 'default'
  @Input() size: ButtonSize = 'default'
  @Input() disabled = false
  @Input() loading = false
  @Input() iconLeft = false
  @Input() iconRight = false
  @Input() fullWidth = false

  @Output() btnClick = new EventEmitter<MouseEvent>()

  /**
   * Build class string based on button properties
   */
  getButtonClasses(): string {
    const baseClasses =
      'inline-flex cursor-pointer items-center justify-center whitespace-nowrap rounded-default font-medium transition-colors focus-visible:outline-none focus-visible:ring-1 focus-visible:ring-ring disabled:pointer-events-none disabled:opacity-50'

    // Size classes
    const sizeClasses = {
      default: 'h-10 px-4 py-2 text-sm',
      sm: 'h-9 px-3 text-xs',
      lg: 'h-11 px-8 text-base',
      icon: 'h-10 w-10 p-0',
    }

    // Variant classes
    const variantClasses = {
      default: 'bg-primary text-primary-foreground hover:bg-primary/90',
      destructive: 'bg-destructive text-destructive-foreground hover:bg-destructive/90',
      outline: 'border border-input bg-background hover:bg-accent hover:text-accent-foreground',
      secondary: 'bg-secondary text-secondary-foreground hover:bg-secondary/80',
      ghost: 'hover:bg-accent hover:text-accent-foreground',
      link: 'text-primary underline-offset-4 hover:underline',
    }

    // Width class
    const widthClass = this.fullWidth ? 'w-full' : ''

    return [baseClasses, sizeClasses[this.size], variantClasses[this.variant], widthClass].join(' ')
  }

  /**
   * Handle button click
   */
  onClick(event: MouseEvent): void {
    if (!this.disabled && !this.loading) {
      this.btnClick.emit(event)
    }
  }
}
