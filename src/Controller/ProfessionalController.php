<?php

namespace App\Controller;

use Symfony\Bundle\FrameworkBundle\Controller\AbstractController;
use Symfony\Component\HttpFoundation\Response;
use Symfony\Component\Routing\Annotation\Route;

class ProfessionalController extends AbstractController
{
    #[Route('/professional', name: 'app_professional')]
    public function index(): Response
    {
        return $this->render('professional/index.html.twig', [
            'controller_name' => 'ProfessionalController',
        ]);
    }
}
