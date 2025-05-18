# Motivation
Most people use Excel for tracking their job application, but I on the other hand decided to use Google Docs. Looking back, I probably should’ve used Excel too... but oh well, it is what it is.

Trying to keep track of everything in Docs was pretty tough, and at the time I didn’t have many personal projects to show. So I thought, why not make a simple Job Tracker!? And well, that's exactly what I did.
Now, it took me longer than expected to finish, but one of my goals (and something a recruiter also recommended) was to stay consistent — to try and do a little something each day.

Also, since I learned full-stack development at uni using .NET and C#, I figured this would be a good chance to dive deeper into it.

Welp, that's the gist of it :)


# Features 
- Create, update, and delete job applications  
- View applications by status (Interested, Applied, Interview, Offer, Rejected)  
- Expand/collapse each job card to see all the details — including the CV and cover letter submitted  

# What I Learned
Not going to lie, I thought I would breeze through this project, but I actually learnt a lot. 

## Blazor Hybrid 
This was my first time working with Blazor Hybrid (ngl I still don’t fully know what that means), but I know it’s component-based, it reminds me of React, and it’s used for building cross-platform desktop/mobile apps.

 I also had to get used to the "flow" of the project. What I mean is: I had to understand what made a specific page the landing page when the app starts, what `@Body` does, why the default route is `/`, and so on. These were the kinds of questions I kept asking myself — the *hows* and *whys* behind what made things work. 🤯

## @bind
Super useful! `@bind` is a two-way data binding directive, which lets us sync a variable with an input element. This made it *extremely* easy to collect form data from the Job Modal and save it to the database.

Since it’s two-way, updating the variable updates the UI, and vice versa. That meant I could also **populate the modal with an existing job application** — making the component reusable for editing!

## Passing data from a parent to a child component - Parameters
Now that I understand it, passing data to a child component is pretty straightforward: add a `[Parameter]` in the child, and bind a value to it in the parent. Bam — data = passed!
- Alternatively, I could’ve used a shared service, but this worked fine.

## Passing data from the child to the parent component - EventCallBack
This one was a bit trickier. You define an `EventCallback<T>` in the child component, and in the parent you bind a function to it. Then, from the child, you call `VarName.InvokeAsync(...)`, and it triggers the parent’s function.
It’s a clean way to send data or events from the child back to the parent, like when I wanted to close the modal or pass a Job application so that it could be saved to the database.

## CascadingParameter
Im still understanding this, but from what I know, CascadingParameter allows descendant components to use values from the parent component (meaning that the component doesn't have to be a direct child of the parent component), the values
can be functions, values, services, objects, etc, the key point of CascadingParameter is that it enables sharing values from parent to child deeply in the tree, even if they're not direct children.

I used this to pass data from the JobAppTracker page which holds all the job applications, to the parent component - MainLayout, where the Job Modal is, so that we can open the modal filled with information relating to a specific 
job application.

```text
MainLayout
├── JobAppTracker
└── JobModal
```

The Hierarchy looks something like above, since JobAppTracker and JobModal aren't directly connected, and i'm not using a shared service to handle passing data between components, using the method explained above seemed like the next 
best alternative.

## Integrating SQLite database
At university, we used `EntityFrameworkCore` to work with SQLite, which involved initial migrations. So naturally, I tried doing the same here... which is essentially what tripped me up when trying to integrate a sqlite DB in this 
application. For this application we used the `sqlite-net-pcl` package, WHICH DID NOT REQUIRE INITIAL MIGRATIONS, and so, everytime I went to add initial migrations, I was met with errors,
which made me think I was doing everything wrong. That led to deleting progress, commits, and redoing things a bunch of times. This is also the main reason why it took me so LOOOONG TO FINISH THIS PROJECT. Anyways, we live and we learn :')

## Error with updating the Cv/Cl Files
Now, this makes no sense to me either, but I’ll try to explain. Updating the other fields of my job application worked fine — the only thing that kept failing was when I tried to update the CV/CL files.

How did I know it failed? Well, every time I tried updating the CV/CL, the exception from the `try-catch` block in `HandleSaveJobApplication` would get thrown. Once I fixed that part, I noticed that the file paths stored in the DB weren’t updating, and the files saved locally to the project were corrupt and unopenable.

So, how did I solve this? I copied and pasted the error into Google (and ChatGPT :<) — and found out that this bug was happening because we were conditionally rendering `<InputFile>`.

Now, this didn’t make sense to me. I was like, _"Wait, don’t we also conditionally render it when we create a job application? So why does it only break when we update one?"_ Still don’t know the answer to that. But hey — if it works, it works.

The fix was pretty straightforward: instead of conditionally rendering `<InputFile>`, we always keep it in the DOM, and just toggle its visibility based on what mode we’re in (Create / Viewing / Editing):

```html
<InputFile id="clInput" class="input" OnChange="HandleCoverLetterUpload" style="@(!IsViewMode ? "" : "display: none;")" /> 
```

# What I’d Do Differently
## Plan more
For this project, I started off by writing down the requirements — what features did I want the app to have? What data should I be storing? How should I store it? Stuff like that.

With some experience from my Human-Computer Interaction course at uni, I decided to design the UI in Figma first, so I wouldn’t have to constantly change the design while building the app.

What I _didn’t_ do was properly plan out the architecture — things like what components talk to what, how services should communicate with each other, and what responsibilities each part of the app should have. I think this was also part of the reason why the project took so long to finish — as I ended up having to make those decisions on the fly.

## Choosing a Project
This was actually advice that was given to me — to choose a project that aligns with my interests, so that I’d stay motivated to work on it over a longer period of time. The second part of the advice was to make it a bit beefy — something that lets you dabble in full-stack development and integrate third-party APIs or services, to show that you're comfortable bringing external tools into your application.

It’s and advice I’ll definitely keep in mind, and hopefully my next project will align even more with what I enjoy :)

## CI/CD GitHub Actions, and Testing
These are definitely areas I’m still lacking in. I didn’t really set up any CI/CD pipeline or write proper tests for this project, but I think for my next one, I want to focus on using both so I can get more comfortable with them and actually understand how they fit into a real workflow.

## Depth >> Breadth
Another piece of advice I got was to go deep into a few tools and technologies instead of learning surface-level stuff about a bunch of them. So with that in mind, I think I’ll try to really build proficiency in 1–2 languages, frameworks, and libraries — get more comfortable and confident with the ones I enjoy using.

# Resources
_TODO_
