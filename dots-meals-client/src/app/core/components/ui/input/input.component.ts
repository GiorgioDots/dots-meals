import { Component, Input, computed, signal, effect, OnInit, OnDestroy } from '@angular/core'
import { CommonModule } from '@angular/common'
import { FormControl, ReactiveFormsModule, FormsModule } from '@angular/forms'
import { NgIcon, provideIcons } from '@ng-icons/core'
import { heroChevronDown } from '@ng-icons/heroicons/outline'
import { RdxSelectModule } from '@radix-ng/primitives/select'
import { EnumOption } from '@/utils/enum-utils'

@Component({
  selector: 'app-input',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, FormsModule, NgIcon, RdxSelectModule],
  templateUrl: './input.component.html',
  styleUrl: './input.component.css',
  providers: [provideIcons({ heroChevronDown })],
})
export class InputComponent implements OnInit, OnDestroy {
  @Input() label = ''
  @Input() type = 'text'
  @Input() placeholder = ''
  @Input() id = ''
  @Input() showError = true
  @Input() options: EnumOption[] = []

  @Input({ required: true }) control!: FormControl

  readonly customInputs = ['select',]

  ngOnInit(): void {
    // Generate a random ID if none provided
    if (!this.id) {
      this.id = `input-${Math.random().toString(36).substring(2, 9)}`
    }
  }

  ngOnDestroy(): void {
    // Angular 19 automatically cleans up effects when component is destroyed
  }

  isInvalid(): boolean {
    return this.control.invalid && (this.control.touched || this.control.dirty)
  }

  getErrorMessage(): string {
    if (!this.control.errors) return ''

    const errors = this.control.errors

    if (errors['required']) {
      return 'This field is required'
    }
    if (errors['email']) {
      return 'Please enter a valid email'
    }
    if (errors['minlength']) {
      return `Minimum length is ${errors['minlength'].requiredLength} characters`
    }
    if (errors['maxlength']) {
      return `Maximum length is ${errors['maxlength'].requiredLength} characters`
    }
    if (errors['pattern']) {
      return 'Invalid format'
    }

    return 'Invalid input'
  }
}
