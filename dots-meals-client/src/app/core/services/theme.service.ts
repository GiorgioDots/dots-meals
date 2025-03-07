import { Injectable, signal } from '@angular/core'

@Injectable({
  providedIn: 'root',
})
export class ThemeService {
  isDarkMode = signal(document.documentElement.getAttribute('data-theme') == 'dark')

  toggleTheme() {
    let theme = localStorage.getItem('app_theme')
    if (theme == null) {
      theme =
        window.matchMedia && window.matchMedia('(prefers-color-scheme: dark)').matches
          ? 'dark'
          : 'light'
    }
    theme = theme == 'dark' ? 'light' : 'dark'
    localStorage.setItem('app_theme', theme)
    document.documentElement.setAttribute('data-theme', theme == 'dark' ? 'abyss' : 'caramellatte')
    this.isDarkMode.set(theme == 'dark')
  }
}
