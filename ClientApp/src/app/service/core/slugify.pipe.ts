import { Injectable, Pipe, PipeTransform } from "@angular/core";

@Pipe({name: 'slugify'})
@Injectable({ providedIn: 'root' })
export class SlugifyPipe implements PipeTransform {
  transform(input: string): string {
    return input.toString().toLowerCase()
      .replace(/\.net\b/, "dot-net")
      .replace(/\s+/g, '-')           // Replace spaces with -
      .replace(/[^\w\-]+/g, '')       // Remove all non-word chars
      .replace(/\-\-+/g, '-')         // Replace multiple - with single -
      .replace(/^-+/, '')             // Trim - from start of text
      .replace(/-+$/, '')            // Trim - from end of text
      .replace(/\.Net\b/, "dotnet")
  }
}