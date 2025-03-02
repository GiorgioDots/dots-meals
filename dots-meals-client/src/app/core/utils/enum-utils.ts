export function getEnumOptions<T extends object>(enumObj: T): EnumOption[] {
  return Object.entries(enumObj)
    .filter(([key, value]) => isNaN(Number(key))) // Exclude numeric indexes
    .map(([key, value]) => ({ value, label: key }))
}

export interface EnumOption {
  value: string | number
  label: string
}
